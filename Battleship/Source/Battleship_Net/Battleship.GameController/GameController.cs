﻿using System.Linq;
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
            validations  = new List<string>();

            foreach (var ship in ships)
            {
                validations.Add($"{ship.Name} overlaps");
            }

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
                if(equalSize == false)
                {
                    validations.Add($"{ship.Name} has incorrect size. Expected: {ship.Size}, Actual: {ship.Positions.Count}");
                }
            }

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
                validations.Add($"{ship.Name} has gap or incorrect position.");
            }

            return isValid;
        }

    }
}