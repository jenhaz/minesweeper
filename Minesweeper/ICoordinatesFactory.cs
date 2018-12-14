using System.Collections.Generic;
using Minesweeper.CoordinatesAround;

namespace Minesweeper
{
    public interface ICoordinatesFactory
    {
        IEnumerable<ICoordinatesAround> GetCoordinatesAround(Limits limits);
    }
}