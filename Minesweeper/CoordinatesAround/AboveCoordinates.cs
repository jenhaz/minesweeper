namespace Minesweeper.CoordinatesAround
{
    public class AboveCoordinates : ICoordinatesAround
    {
        public bool Check(Coordinates inputCoordinates, Coordinates mineCoordinates)
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

        private bool IsMine(Coordinates input, Coordinates mine)
        {
            return input.X == mine.X && input.Y == mine.Y;
        }
    }
}
