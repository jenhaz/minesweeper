namespace Minesweeper.CoordinatesAround
{
    public class RightCoordinates : ICoordinatesAround
    {
        private readonly Limits _limits;

        public RightCoordinates(Limits limits)
        {
            _limits = limits;
        }

        public bool Check(Coordinates inputCoordinates, Coordinates mineCoordinates)
        {
            var x = inputCoordinates.X;

            var columnRight = x + 1 != _limits.X ? x + 1 : x;

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

        private bool IsMine(Coordinates input, Coordinates mine)
        {
            return input.X == mine.X && input.Y == mine.Y;
        }
    }
}