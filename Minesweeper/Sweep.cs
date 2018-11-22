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

        public bool CheckAreaForMine(
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