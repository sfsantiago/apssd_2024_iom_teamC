﻿
namespace Battleship.Ascii
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Battleship.GameController;
    using Battleship.GameController.Contracts;

    internal class Program
    {
        private static List<Ship> myFleet;

        private static List<Ship> enemyFleet;

        static void Main()
        {
            Console.WriteLine("                                     |__");
            Console.WriteLine(@"                                     |\/");
            Console.WriteLine("                                     ---");
            Console.WriteLine("                                     / | [");
            Console.WriteLine("                              !      | |||");
            Console.WriteLine("                            _/|     _/|-++'");
            Console.WriteLine("                        +  +--|    |--|--|_ |-");
            Console.WriteLine(@"                     { /|__|  |/\__|  |--- |||__/");
            Console.WriteLine(@"                    +---------------___[}-_===_.'____                 /\");
            Console.WriteLine(@"                ____`-' ||___-{]_| _[}-  |     |_[___\==--            \/   _");
            Console.WriteLine(@" __..._____--==/___]_|__|_____________________________[___\==--____,------' .7");
            Console.WriteLine(@"|                        Welcome to Battleship                         BB-61/");
            Console.WriteLine(@" \_________________________________________________________________________|");
            Console.WriteLine();

            bool continueGame = InitializeGame();
            if (continueGame)
            {
                StartGame();
            }
        }

        private static void StartGame()
        {
            Console.Clear();
            Console.WriteLine("                  __");
            Console.WriteLine(@"                 /  \");
            Console.WriteLine("           .-.  |    |");
            Console.WriteLine(@"   *    _.-'  \  \__/");
            Console.WriteLine(@"    \.-'       \");
            Console.WriteLine("   /          _/");
            Console.WriteLine(@"  |      _  /""");
            Console.WriteLine(@"  |     /_\'");
            Console.WriteLine(@"   \    \_/");
            Console.WriteLine(@"    """"""""");

            do
            {
                List<string> validations = new List<string>();

                Console.WriteLine();
                Console.WriteLine("Player, it's your turn");
                addCoordinate:
                Console.WriteLine("Enter coordinates for your shot :");
                var position = TryParsePosition(Console.ReadLine(), out validations);
                if(position == null)
                {
                    foreach (var item in validations)
                    {
                        Console.WriteLine(item);
                    }
                    validations.Clear();
                    goto addCoordinate;
                }
                var isHit = GameController.CheckIsHit(enemyFleet, position);
                if (isHit)
                {
                    Console.Beep();

                    Console.WriteLine(@"                \         .  ./");
                    Console.WriteLine(@"              \      .:"";'.:..""   /");
                    Console.WriteLine(@"                  (M^^.^~~:.'"").");
                    Console.WriteLine(@"            -   (/  .    . . \ \)  -");
                    Console.WriteLine(@"               ((| :. ~ ^  :. .|))");
                    Console.WriteLine(@"            -   (\- |  \ /  |  /)  -");
                    Console.WriteLine(@"                 -\  \     /  /-");
                    Console.WriteLine(@"                   \  \   /  /");
                }

                Console.WriteLine(isHit ? "Yeah ! Nice hit !" : "Miss");

                position = GetRandomPosition();
                isHit = GameController.CheckIsHit(myFleet, position);
                Console.WriteLine();
                Console.WriteLine("Computer shot in {0}{1} and {2}", position.Column, position.Row, isHit ? "has hit your ship !" : "miss");
                if (isHit)
                {
                    Console.Beep();

                    Console.WriteLine(@"                \         .  ./");
                    Console.WriteLine(@"              \      .:"";'.:..""   /");
                    Console.WriteLine(@"                  (M^^.^~~:.'"").");
                    Console.WriteLine(@"            -   (/  .    . . \ \)  -");
                    Console.WriteLine(@"               ((| :. ~ ^  :. .|))");
                    Console.WriteLine(@"            -   (\- |  \ /  |  /)  -");
                    Console.WriteLine(@"                 -\  \     /  /-");
                    Console.WriteLine(@"                   \  \   /  /");

                }
            }
            while (true);
        }

        internal static Position ParsePosition(string input)
        {
            var letter = (Letters)Enum.Parse(typeof(Letters), input.ToUpper().Substring(0, 1));
            var number = int.Parse(input.Substring(1, 1));
            return new Position(letter, number);
        }

        internal static Position TryParsePosition(string input, out List<string> validations)
        {
            validations = new List<string>();
            try
            {
                return ParsePosition(input);
            }
            catch (Exception ex)
            {
                validations.Add("Invalid Position.");
                return null;
            }
        }

        private static Position GetRandomPosition()
        {
            int rows = 8;
            int lines = 8;
            var random = new Random();
            var letter = (Letters)random.Next(lines);
            var number = random.Next(rows);
            var position = new Position(letter, number);
            return position;
        }

        private static bool InitializeGame()
        {
            bool continueGame = InitializeMyFleet();
            InitializeEnemyFleet();
            return continueGame;
        }

        private static bool InitializeMyFleet()
        {
            bool continueGame = false;

            myFleet = GameController.InitializeShips().ToList();

            Console.WriteLine("Please position your fleet (Game board size is from A to H and 1 to 8) :");

            setUpShip:
            List<string> validations = new List<string>();
            foreach (var ship in myFleet)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter the positions for the {0} (size: {1})", ship.Name, ship.Size);
                for (var i = 1; i <= ship.Size; i++)
                {
                    Console.WriteLine("Enter position {0} of {1} (i.e A3):", i, ship.Size);
                    validations = new List<string>();
                    if (ship.TryAddPosition(Console.ReadLine(), out validations) == false)
                    {
                        foreach (var validation in validations)
                        {
                            Console.WriteLine(validation);
                        }
                        i -= 1;
                    }
                }
            }

            List<string> allValidations = new List<string>();
            if (GameController.TryValidateShipSize(myFleet, out validations) == false)
            {
                allValidations.AddRange(validations);
                foreach (var validation in validations)
                {
                    Console.WriteLine(validation);
                }
            }
            if (GameController.TryValidateShipPosition(myFleet, out validations) == false)
            {
                allValidations.AddRange(validations);
                foreach (var validation in validations)
                {
                    Console.WriteLine(validation);
                }
            }
            if (GameController.TryValidateOverlap(myFleet, out validations) == false)
            {
                allValidations.AddRange(validations);
                foreach (var validation in validations)
                {
                    Console.WriteLine(validation);
                }
            }

            if (allValidations.Any() == false)
            {
                Console.WriteLine("All ships are set!");
            }


            nextUserInput:
            Console.WriteLine("");

            Console.WriteLine("Change Ship Position:");
            Console.WriteLine("[1] Aircraft Carrier");
            Console.WriteLine("[2] Battleship");
            Console.WriteLine("[3] Submarine");
            Console.WriteLine("[4] Destroyer");
            Console.WriteLine("[5] Patrol Boat");

            Console.WriteLine("");
            Console.WriteLine("[Y] Start Game");
            Console.WriteLine("[N] Exit Game");

            string userInput = Console.ReadLine();
            int shipNumber = -1;
            int.TryParse(userInput, out shipNumber);
            
            if (String.Equals(userInput, "Y", StringComparison.OrdinalIgnoreCase))
            {
                continueGame = true;
            }
            else if (String.Equals(userInput, "N", StringComparison.OrdinalIgnoreCase))
            {
                continueGame = false;
            }
            else if (new[] {1,2,3,4,5}.Contains(shipNumber))
            {
                goto setUpShip;
            }
            else
            {
                goto nextUserInput;
            }

            return continueGame;
        }

        private static void InitializeEnemyFleet()
        {
            enemyFleet = GameController.InitializeShips().ToList();

            enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 4 });
            enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 5 });
            enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 6 });
            enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 7 });
            enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 8 });

            enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 6 });
            enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 7 });
            enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 8 });
            enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 9 });

            enemyFleet[2].Positions.Add(new Position { Column = Letters.A, Row = 3 });
            enemyFleet[2].Positions.Add(new Position { Column = Letters.B, Row = 3 });
            enemyFleet[2].Positions.Add(new Position { Column = Letters.C, Row = 3 });

            enemyFleet[3].Positions.Add(new Position { Column = Letters.F, Row = 8 });
            enemyFleet[3].Positions.Add(new Position { Column = Letters.G, Row = 8 });
            enemyFleet[3].Positions.Add(new Position { Column = Letters.H, Row = 8 });

            enemyFleet[4].Positions.Add(new Position { Column = Letters.C, Row = 5 });
            enemyFleet[4].Positions.Add(new Position { Column = Letters.C, Row = 6 });
        }
    }
}
