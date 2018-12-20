using System.Collections.Generic;

namespace Minesweeper.CoordinatesAround
{
    public class CoordinatesAroundAroundFactory : ICoordinatesAroundFactory
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
                new BelowRightCoordinates(limits),
                new LeftCoordinates(),
                new RightCoordinates(limits)
            };
        }
    }
}