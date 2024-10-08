using System.Linq;
using System.Windows.Media;

namespace Battleship.GameController
{
    using System;
    using System.Collections.Generic;

    using Battleship.GameController.Contracts;

    /// <summary>
    ///     The game controller.
    /// </summary>
    public class GameController
    {
        /// <summary>
        /// Checks the is hit.
        /// </summary>
        /// <param name="ships">
        /// The ships.
        /// </param>
        /// <param name="shot">
        /// The shot.
        /// </param>
        /// <returns>
        /// True if hit, else false
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// ships
        ///     or
        ///     shot
        /// </exception>
        public static bool CheckIsHit(IEnumerable<Ship> ships, Position shot)
        {
            if (ships == null)
            {
                throw new ArgumentNullException("ships");
            }

            if (shot == null)
            {
                throw new ArgumentNullException("shot");
            }

            foreach (var ship in ships)
            {
                foreach (var position in ship.Positions)
                {
                    if (position.Equals(shot))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        ///     The initialize ships.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public static IEnumerable<Ship> InitializeShips()
        {
            return new List<Ship>()
                       {
                           new Ship() { Name = "Aircraft Carrier", Size = 5, Color = Colors.CadetBlue },
                           new Ship() { Name = "Battleship", Size = 4, Color = Colors.Red },
                           new Ship() { Name = "Submarine", Size = 3, Color = Colors.Chartreuse },
                           new Ship() { Name = "Destroyer", Size = 3, Color = Colors.Yellow },
                           new Ship() { Name = "Patrol Boat", Size = 2, Color = Colors.Orange }
                       };
        }

        /// <summary>
        /// The is ships valid.
        /// </summary>
        /// <param name="ship">
        /// The ship.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsShipValid(Ship ship)
        {
            return ship.Positions.Count == ship.Size;
        }

        public static Position GetRandomPosition(int size)
        {
            var random = new Random();
            var letter = (Letters)random.Next(size);
            var number = random.Next(size);
            var position = new Position(letter, number);
            return position;
        }

        /// <summary>
        /// Ships must not overlap each other
        /// </summary>
        /// <param name="ships"></param>
        /// <param name="validations"></param>
        /// <returns></returns>
        public static bool TryValidateOverlap(List<Ship> ships, out List<string> validations)
        {
            bool isValid = false;
            validations = new List<string>();

            if (ships.Count < 2)
            {
                isValid = true;
                return isValid;
            }

            for (int i = 0; i < ships.Count - 1; i++)
            {
                for (int j = 1; j < ships.Count; j++)
                {
                    if (ships[i].Name == ships[j].Name)
                    {
                        continue;
                    }

                    var shipi = ships[i].Positions;
                    var shipj = ships[j].Positions;

                    var overlappingShip = shipi.Where(n1 => shipj.Where(n2 => n1.Row == n2.Row && n1.Column == n2.Column).Any()).FirstOrDefault();
                    if (overlappingShip != null)
                    {
                        validations.Add($"{ships[i].Name} overlaps {ships[j].Name}.");
                    }
                }
            }
            isValid = validations.Any() == false;

            return isValid;
        }
        /// <summary>
        /// All ships have the correct size
        /// </summary>
        /// <param name="ships"></param>
        /// <param name="validations"></param>
        /// <returns></returns>
        public static bool TryValidateShipSize(List<Ship> ships, out List<string> validations)
        {
            bool isValid = false;
            validations = new List<string>();

            foreach (var ship in ships)
            {

                bool equalSize = ship.Positions.Count == ship.Size;
                if (equalSize == false)
                {
                    validations.Add($"{ship.Name} has incorrect size. Expected: {ship.Size}, Actual: {ship.Positions.Count}");
                }
            }
            isValid = validations.Any() == false;

            return isValid;
        }
        /// <summary>
        /// All ships have all positions in a horizontal or vertical row, gaps are not allowed
        /// </summary>
        /// <param name="ships"></param>
        /// <param name="validations"></param>
        /// <returns></returns>
        public static bool TryValidateShipPosition(List<Ship> ships, out List<string> validations)
        {
            bool isValid = false;
            validations = new List<string>();

            foreach (var ship in ships)
            {
                Position firstPosition = ship.Positions.FirstOrDefault();
                Position lastPosition = ship.Positions.LastOrDefault();

                if (firstPosition == null)
                {
                    validations.Add($"{ship.Name} has no first position.");
                    break;
                }
                if (lastPosition == null)
                {
                    validations.Add($"{ship.Name} has no last position.");
                    break;
                }

                bool isHorizontal = ship.Positions.All(n => n.Row == firstPosition.Row);
                bool isVertical = ship.Positions.All(n => n.Column == firstPosition.Column);

                if (isHorizontal == false && isVertical == false)
                {
                    validations.Add($"{ship.Name} has incorrect position.");
                    break;
                }

                if (isHorizontal)
                {
                    int column = (int)firstPosition.Column;
                    foreach (var pos in ship.Positions.OrderBy(n => (int)n.Column))
                    {
                        if ((int)pos.Column != column)
                        {
                            validations.Add($"{ship.Name} has horizontal gap in position.");
                            break;
                        }
                        column += 1;
                    }
                }
                if (isVertical)
                {
                    int row = firstPosition.Row;
                    foreach (var pos in ship.Positions.OrderBy(n => n.Row))
                    {
                        if (pos.Row != row)
                        {
                            validations.Add($"{ship.Name} has vertical gap in position.");
                            break;
                        }
                        row += 1;
                    }
                }
            }


            isValid = validations.Any() == false;
            return isValid;
        }

    }
}