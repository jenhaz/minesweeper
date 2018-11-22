using System.Linq;

namespace Minesweeper
{
    public class Sweep
    {
        public Coordinates GetCoordinates(string input)
        {
            var coordinates = input.Split('#').Last();
            var x = coordinates[0];

            if (!char.IsLetter(x) ||
                coordinates.Length > 3)
            {
                return null;
            }

            if (coordinates.Length == 2)
            {
                x = coordinates[0];
                var y = (coordinates[1] - '1') + 1;

                return ConvertToCoordinates(x, y);
            }

            if (coordinates.Length == 3)
            {
                x = coordinates[0];
                var y = int.Parse(coordinates.Substring(1, 2));

                return ConvertToCoordinates(x, y);
            }

            return null;
        }

        public bool IsMine(Coordinates input, string mine)
        {
            var mineCoordinates = GetCoordinates(mine);

            return input.X == mineCoordinates.X && input.Y == mineCoordinates.Y;
        }

        public bool IsMine(Coordinates input, Coordinates mine)
        {
            return input.X == mine.X && input.Y == mine.Y;
        }

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
            var coordinates = GetCoordinates(input);
            
            if (coordinates?.X > xLimit || coordinates?.Y > yLimit)
            {
                return false;
            }

            return true;
        }
        
        private Coordinates ConvertToCoordinates(char xCoordinate, int yCoordinate)
        {
            return new Coordinates
            {
                X = xCoordinate % 32,
                Y = yCoordinate
            };
        }

        public bool CheckAroundForMine(
            Coordinates inputCoordinates, 
            Coordinates mineCoordinates,
            int xLimit,
            int yLimit)
        {
            var isMineSomewhere = CheckAboveCoordinates(inputCoordinates, mineCoordinates) || 
                              CheckBelowCoordinates(inputCoordinates, mineCoordinates, yLimit) || 
                              CheckLeftCoordinates(inputCoordinates, mineCoordinates) || 
                              CheckRightCoordinates(inputCoordinates, mineCoordinates, xLimit);

            return isMineSomewhere;
        }

        public bool CheckAboveCoordinates(Coordinates inputCoordinates, Coordinates mineCoordinates)
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

        public bool CheckLeftCoordinates(Coordinates inputCoordinates, Coordinates mineCoordinates)
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