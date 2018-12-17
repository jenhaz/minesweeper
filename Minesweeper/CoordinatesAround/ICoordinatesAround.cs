namespace Minesweeper.CoordinatesAround
{
    public interface ICoordinatesAround
    {
        bool Check(Coordinates input, Coordinates mine);
        Coordinates Get(Coordinates input);
    }
}