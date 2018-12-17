namespace Minesweeper.CoordinatesAround
{
    public class LeftCoordinates : ICoordinatesAround
    {
        public bool Check(Coordinates inputCoordinates, Coordinates mineCoordinates)
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

        public Coordinates Get(Coordinates input)
        {
            return new Coordinates
            {
                X = input.X - 1 != 0 ? input.X - 1 : 0,
                Y = input.Y
            };
        }

        private bool IsMine(Coordinates input, Coordinates mine)
        {
            return input.X == mine.X && input.Y == mine.Y;
        }
    }
}