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
                new AboveLeftCoordinates(),
                new AboveRightCoordinates(limits),
                new BelowCoordinates(limits),
                new BelowLeftCoordinates(limits),
                new LeftCoordinates(),
                new RightCoordinates(limits)
            };
        }
    }
}