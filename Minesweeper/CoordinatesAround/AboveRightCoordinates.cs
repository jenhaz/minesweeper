namespace Minesweeper.CoordinatesAround
{
    public class AboveRightCoordinates : ICoordinatesAround
    {
        private readonly Limits _limits;

        public AboveRightCoordinates(Limits limits)
        {
            _limits = limits;
        }

        public bool Check(Coordinates input, Coordinates mine)
        {
            var aboveRightCoordinates = new Coordinates();

            var columnRight = input.X + 1 != _limits.X 
                ? input.X + 1 
                : input.X;

            if (columnRight == input.X)
            {
                return false;
            }

            aboveRightCoordinates.X = columnRight;

            var rowAbove = input.Y - 1 != 0 
                ? input.Y - 1 
                : 0;

            if (rowAbove == 0)
            {
                return false;
            }

            aboveRightCoordinates.Y = rowAbove;

            return IsMine(aboveRightCoordinates, mine);
        }

        public Coordinates Get(Coordinates input)
        {
            return new Coordinates
            {
                X = input.X + 1 != _limits.X ? input.X + 1 : input.X,
                Y = input.Y - 1 != 0 ? input.Y - 1 : 0
            };
        }

        private bool IsMine(Coordinates input, Coordinates mine)
        {
            return input.X == mine.X && input.Y == mine.Y;
        }
    }
}