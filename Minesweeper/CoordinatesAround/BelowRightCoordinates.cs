namespace Minesweeper.CoordinatesAround
{
    public class BelowRightCoordinates : ICoordinatesAround
    {
        private readonly Limits _limits;

        public BelowRightCoordinates(Limits limits)
        {
            _limits = limits;
        }

        public bool Check(Coordinates input, Coordinates mine)
        {
            var belowRightCoordinates = new Coordinates();

            var columnRight = input.X + 1 != _limits.X
                ? input.X + 1
                : input.X;

            if (columnRight == input.X)
            {
                return false;
            }

            belowRightCoordinates.X = columnRight;

            var rowBelow = input.Y + 1 != _limits.Y 
                ? input.Y + 1 
                : input.Y;

            if (rowBelow == input.Y)
            {
                return false;
            }

            belowRightCoordinates.Y = rowBelow;

            return IsMine(belowRightCoordinates, mine);
        }

        public Coordinates Get(Coordinates input)
        {
            return new Coordinates
            {
                X = input.X + 1 != _limits.X ? input.X + 1 : input.X,
                Y = input.Y + 1 != _limits.Y ? input.Y + 1 : input.Y
            };
        }

        private bool IsMine(Coordinates input, Coordinates mine)
        {
            return input.X == mine.X && input.Y == mine.Y;
        }
    }
}
