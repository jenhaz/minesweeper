namespace Minesweeper.CoordinatesAround
{
    public class BelowCoordinates : ICoordinatesAround
    {
        private readonly Limits _limits;

        public BelowCoordinates(Limits limits)
        {
            _limits = limits;
        }

        public bool Check(Coordinates inputCoordinates, Coordinates mineCoordinates)
        {
            var y = inputCoordinates.Y;

            var rowBelow = y + 1 != _limits.Y ? y + 1 : y;

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

        public Coordinates Get(Coordinates input)
        {
            return new Coordinates
            {
                X = input.X,
                Y = input.Y + 1 != _limits.Y ? input.Y + 1 : input.Y
            };
        }

        private bool IsMine(Coordinates input, Coordinates mine)
        {
            return input.X == mine.X && input.Y == mine.Y;
        }
    }
}