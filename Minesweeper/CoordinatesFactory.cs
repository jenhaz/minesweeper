using System.Collections.Generic;
using Minesweeper.CoordinatesAround;

namespace Minesweeper
{
    public class CoordinatesFactory : ICoordinatesFactory
    {
        public IEnumerable<ICoordinatesAround> GetCoordinatesAround(Limits limits)
        {
            return new List<ICoordinatesAround>
            {
                new AboveCoordinates(),
                new BelowCoordinates(limits),
                new LeftCoordinates(),
                new RightCoordinates(limits)
            };
        }
    }
}