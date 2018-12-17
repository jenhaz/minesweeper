namespace Minesweeper.CoordinatesAround
{
    public class AboveLeftCoordinates : ICoordinatesAround
    {
        public bool Check(Coordinates input, Coordinates mine)
        {
            var aboveLeftCoordinates = new Coordinates();

            var columnLeft = input.X - 1 != 0 
                ? input.X - 1 
                : 0;

            if (columnLeft == 0)
            {
                return false;
            }

            aboveLeftCoordinates.X = columnLeft;

            var rowAbove = input.Y - 1 != 0
                ? input.Y - 1
                : 0;

            if (rowAbove == 0)
            {
                return false;
            }

            aboveLeftCoordinates.Y = rowAbove;

            return IsMine(aboveLeftCoordinates, mine);
        }

        public Coordinates Get(Coordinates input)
        {
            return new Coordinates
            {
                X = input.X - 1 != 0 ? input.X - 1 : 0,
                Y = input.Y - 1 != 0 ? input.Y - 1 : 0
            };
        }

        private bool IsMine(Coordinates input, Coordinates mine)
        {
            return input.X == mine.X && input.Y == mine.Y;
        }
    }
}
