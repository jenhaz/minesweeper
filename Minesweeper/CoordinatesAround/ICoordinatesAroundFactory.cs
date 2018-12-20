using System.Collections.Generic;

namespace Minesweeper.CoordinatesAround
{
    public interface ICoordinatesAroundFactory
    {
        IEnumerable<ICoordinatesAround> GetCoordinatesAround(Limits limits);
    }
}