
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
        setUpGame:
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
            Console.Write(@"\                      ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("BRP ASIM KILIG - CNIGANG");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                            |");
            Console.WriteLine(@" \_________________________________________________________________________|");
            Console.WriteLine();

            bool continueGame = InitializeGame();
            bool playAgain = false;
            if (continueGame)
            {
                playAgain = StartGame();
            }
            if (playAgain)
            {
                goto setUpGame;
            }
        }

        private static bool StartGame()
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

            bool hasWinner = false;
            bool playAgain = false;

            do
            {
                List<string> validations = new List<string>();

                Console.WriteLine();
                Console.WriteLine("Player, it's your turn");
            addCoordinate:
                Console.WriteLine("Enter coordinates for your shot :");
                var position = TryParsePosition(Console.ReadLine(), out validations);
                if (position == null)
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
                    Divider();
                    DisplayHitShip(enemyFleet, ConsoleColor.Green);
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
                    Divider();
                    DisplayHitShip(myFleet, ConsoleColor.Red);
                }

                if (enemyFleet.All(n => n.Sunk))
                {
                    foreach (var ship in enemyFleet.Where(n => n.Sunk))
                    {
                        Console.WriteLine($"{ship.Name} sunk!");
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Enemy defeated!");
                    Console.WriteLine("Player win!");
                    Console.ForegroundColor = ConsoleColor.White;

                    hasWinner = true;
                }
                else if (myFleet.All(n => n.Sunk))
                {
                    foreach (var ship in enemyFleet.Where(n => n.Sunk))
                    {
                        Console.WriteLine($"{ship.Name} sunk!");
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You are defeated!");
                    Console.WriteLine("Computer win!");
                    Console.ForegroundColor = ConsoleColor.White;

                    hasWinner = true;
                }
            }
            while (hasWinner == false);

            if (hasWinner)
            {
                Console.WriteLine("Player's ships");
                foreach (var ship in myFleet)
                {
                    Console.WriteLine($"{ship.Name} at position {String.Join(", ", ship.Positions.Select(n => n.ToString() + (n.IsHit ? "(hit)" : "")))}");
                }
                Console.WriteLine("Computer's ships");
                foreach (var ship in enemyFleet)
                {
                    Console.WriteLine($"{ship.Name} at position {String.Join(", ", ship.Positions.Select(n => n.ToString() + (n.IsHit ? "(hit)" : "")))}");
                }


            nextUserInput:
                Console.WriteLine("[Y] Play Again?");
                Console.WriteLine("[N] Exit Game");

                string userInput = Console.ReadLine();
                if (String.Equals(userInput, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    playAgain = true;
                }
                else if (String.Equals(userInput, "N", StringComparison.OrdinalIgnoreCase))
                {
                    playAgain = false;
                }
                else
                {
                    goto nextUserInput;
                }
            }

            return playAgain;
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

            string shipName = string.Empty;
            Dictionary<int, string> ships = new Dictionary<int, string>();
            ships.Add(1, "Aircraft Carrier");
            ships.Add(2, "Battleship");
            ships.Add(3, "Submarine");
            ships.Add(4, "Destroyer");
            ships.Add(5, "Patrol Boat");

        setUpShip:
            List<string> validations = new List<string>();
            foreach (var ship in myFleet.Where(n => String.IsNullOrWhiteSpace(shipName) || String.Equals(n.Name, shipName, StringComparison.OrdinalIgnoreCase)))
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
                Divider();
                foreach (var item in myFleet)
                {
                    Console.WriteLine($"{item.Name}, ready!");
                }
                Console.WriteLine();
                Console.WriteLine("All ships are set!");
                Divider();
            }


        nextUserInput:
            Divider();

            Console.WriteLine("Change Ship Position:");
            Console.WriteLine("[1] Aircraft Carrier");
            Console.WriteLine("[2] Battleship");
            Console.WriteLine("[3] Submarine");
            Console.WriteLine("[4] Destroyer");
            Console.WriteLine("[5] Patrol Boat");

            Console.WriteLine("");
            if (allValidations.Any() == false)
            {
                Console.WriteLine("[Y] Start Game");
            }
            Console.WriteLine("[N] Exit Game");

            Divider();

            string userInput = Console.ReadLine();
            int shipNumber = -1;
            int.TryParse(userInput, out shipNumber);

            if (String.Equals(userInput, "Y", StringComparison.OrdinalIgnoreCase))
            {
                if (allValidations.Any())
                {
                    goto nextUserInput;
                }
                continueGame = true;
            }
            else if (String.Equals(userInput, "N", StringComparison.OrdinalIgnoreCase))
            {
                continueGame = false;
            }
            else if (ships.Select(n => n.Key).Contains(shipNumber))
            {
                shipName = ships.Where(n => n.Key == shipNumber).Select(n => n.Value).FirstOrDefault();
                foreach (var item in myFleet.Where(n => n.Name == shipName))
                {
                    item.Positions.Clear();
                }

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

            enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 5 });
            enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 6 });
            enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 7 });
            enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 8 });

            enemyFleet[2].Positions.Add(new Position { Column = Letters.A, Row = 3 });
            enemyFleet[2].Positions.Add(new Position { Column = Letters.B, Row = 3 });
            enemyFleet[2].Positions.Add(new Position { Column = Letters.C, Row = 3 });

            enemyFleet[3].Positions.Add(new Position { Column = Letters.F, Row = 8 });
            enemyFleet[3].Positions.Add(new Position { Column = Letters.G, Row = 8 });
            enemyFleet[3].Positions.Add(new Position { Column = Letters.H, Row = 8 });

            enemyFleet[4].Positions.Add(new Position { Column = Letters.C, Row = 5 });
            enemyFleet[4].Positions.Add(new Position { Column = Letters.C, Row = 6 });
        }

        static void Divider()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new String('*', 50));
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void DisplayHitShip(IEnumerable<Ship> ships, ConsoleColor consoleColor)
        {
            var hitShips = ships.Where(n => n.IsLastHit == true);
            if (hitShips.Any())
            {
                Console.ForegroundColor = consoleColor;
                var ship = hitShips.FirstOrDefault();
                Console.Write($"{ship.Name} is hit ");
                if (ship.LastHitPosition != null)
                {
                    Console.WriteLine($"at position {ship.LastHitPosition.ToString()}.");
                }
                Console.WriteLine($"{ship.Name} {ship.Positions.Where(n => n.IsHit).Count()} of {ship.Positions.Count()}");
                if (ship.Sunk)
                {
                    Console.WriteLine($"{ship.Name} sunk!");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
