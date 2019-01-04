namespace Minesweeper.CoordinatesAround
{
    public class RightCoordinates : ICoordinatesAround
    {
        private readonly Limits _limits;

        public RightCoordinates(Limits limits)
        {
            _limits = limits;
        }

        public bool Check(Coordinates input, Coordinates mine)
        {
            var x = input.X;

            var columnRight = x + 1 != _limits.X ? x + 1 : x;

            if (columnRight == x)
            {
                return false;
            }

            var rightCoordinates = new Coordinates
            {
                X = columnRight,
                Y = input.Y
            };

            return new Mine().IsMine(rightCoordinates, mine);
        }

        public Coordinates Get(Coordinates input)
        {
            return new Coordinates
            {
                X = input.X + 1 != _limits.X ? input.X + 1 : input.X,
                Y = input.Y
            };
        }
    }
}