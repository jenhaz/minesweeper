namespace Minesweeper.CoordinatesAround
{
    public class BelowLeftCoordinates : ICoordinatesAround
    {
        private readonly Limits _limits;

        public BelowLeftCoordinates(Limits limits)
        {
            _limits = limits;
        }

        public bool Check(Coordinates input, Coordinates mine)
        {
            var belowLeftCoordinates = new Coordinates();

            var rowBelow = input.Y + 1 != _limits.Y ? input.Y + 1 : input.Y;

            if (rowBelow == input.Y)
            {
                return false;
            }

            belowLeftCoordinates.Y = rowBelow;

            var columnLeft = input.X - 1 != 0 ? input.X - 1 : 0;

            if (columnLeft == 0)
            {
                return false;
            }

            belowLeftCoordinates.X = columnLeft;

            return new Mine().IsMine(belowLeftCoordinates, mine);
        }

        public Coordinates Get(Coordinates input)
        {
            return new Coordinates
            {
                X = input.X - 1 != 0 ? input.X - 1 : 0,
                Y = input.Y + 1 != _limits.Y ? input.Y + 1 : input.Y
            };
        }
    }
}
