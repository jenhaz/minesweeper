namespace Minesweeper.CoordinatesAround
{
    public interface ICoordinatesAround
    {
        bool Check(Coordinates inputCoordinates, Coordinates mineCoordinates);
    }
}