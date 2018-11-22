﻿using System.Linq;

namespace Minesweeper
{
    public class Validate
    {
        public bool InputIsValid(string input)
        {
            var coordinates = input.Split('#').Last();

            if (coordinates.Any(c => !char.IsLetterOrDigit(c)))
            {
                return false;
            }

            return true;
        }

        public bool InputIsWithinRange(string input, int xLimit, int yLimit)
        {
            var coordinates = new Coordinates().Get(input);

            if (coordinates?.X > xLimit || coordinates?.Y > yLimit)
            {
                return false;
            }

            return true;
        }
    }
}