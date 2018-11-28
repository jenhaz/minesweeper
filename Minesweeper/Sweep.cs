using System.Collections.Generic;

namespace Minesweeper
{
    public class Sweep
    {
        public bool IsMine(Coordinates input, string mine)
        {
            var mineCoordinates = new Coordinates().Get(mine);

            return input.X == mineCoordinates.X && input.Y == mineCoordinates.Y;
        }

        public bool IsMine(Coordinates input, Coordinates mine)
        {
            return input.X == mine.X && input.Y == mine.Y;
        }
        
        public List<Coordinates> GetCoordinatesAroundInput(
            Coordinates input,
            Limits limits)
        {
            var result = new List<Coordinates>();

            var y = input.Y;
            var x = input.X;

            result.Add(new Coordinates
            {
                X = x,
                Y = y - 1 != 0 ? y - 1 : 0
            });

            result.Add(new Coordinates
            {
                X = x,
                Y = y + 1 != limits.Y ? y + 1 : y
            });

            result.Add(new Coordinates
            {
                X = x - 1 != 0 ? x - 1 : 0,
                Y = y
            });

            result.Add(new Coordinates
            {
                X = x + 1 != limits.X ? x + 1 : x,
                Y = y
            });

            return result;
        }
        
        public bool CheckAreaForMine(
            Coordinates inputCoordinates,
            Coordinates mineCoordinates,
            Limits limits)
        {
            var isMineSomewhere = CheckAboveCoordinates(inputCoordinates, mineCoordinates) ||
                                  CheckBelowCoordinates(inputCoordinates, mineCoordinates, limits.Y) ||
                                  CheckLeftCoordinates(inputCoordinates, mineCoordinates) ||
                                  CheckRightCoordinates(inputCoordinates, mineCoordinates, limits.X);

            return isMineSomewhere;
        }

        public bool CheckAboveCoordinates(
            Coordinates inputCoordinates, 
            Coordinates mineCoordinates)
        {
            var y = inputCoordinates.Y;

            var rowAbove = y - 1 != 0 ? y - 1 : 0;

            if (rowAbove == 0)
            {
                return false;
            }

            var aboveCoordinates = new Coordinates
            {
                X = inputCoordinates.X,
                Y = rowAbove
            };

            return IsMine(aboveCoordinates, mineCoordinates);
        }

        public bool CheckBelowCoordinates(
            Coordinates inputCoordinates, 
            Coordinates mineCoordinates,
            int yLimit)
        {
            var y = inputCoordinates.Y;

            var rowBelow = y + 1 != yLimit ? y + 1 : y;

            if (rowBelow == y)
            {
                return false;
            }

            var belowCoordinates = new Coordinates
            {
                X = inputCoordinates.X,
                Y = rowBelow
            };

            return IsMine(belowCoordinates, mineCoordinates);
        }

        public bool CheckLeftCoordinates(
            Coordinates inputCoordinates, 
            Coordinates mineCoordinates)
        {
            var x = inputCoordinates.X;

            var columnLeft = x - 1 != 0 ? x - 1 : 0;

            if (columnLeft == 0)
            {
                return false;
            }

            var leftCoordinates = new Coordinates
            {
                X = columnLeft,
                Y = inputCoordinates.Y
            };

            return IsMine(leftCoordinates, mineCoordinates);
        }

        public bool CheckRightCoordinates(
            Coordinates inputCoordinates,
            Coordinates mineCoordinates,
            int xLimit)
        {
            var x = inputCoordinates.X;

            var columnRight = x + 1 != xLimit ? x + 1 : x;

            if (columnRight == x)
            {
                return false;
            }

            var rightCoordinates = new Coordinates
            {
                X = columnRight,
                Y = inputCoordinates.Y
            };

            return IsMine(rightCoordinates, mineCoordinates);
        }
    }
}