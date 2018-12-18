using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    public class Sweep
    {
        private readonly ICoordinatesFactory _coordinatesFactory;

        public Sweep(ICoordinatesFactory coordinatesFactory)
        {
            _coordinatesFactory = coordinatesFactory;
        }

        public bool IsMine(Coordinates input, IEnumerable<Coordinates> mines)
        {
            return mines.Any(mine => input.X == mine.X && input.Y == mine.Y);
        }

        public IEnumerable<Coordinates> GetCoordinatesAroundInput(
            Coordinates input,
            Limits limits)
        {
            return _coordinatesFactory.GetCoordinatesAround(limits)
                .Select(direction => direction.Get(input))
                .ToList();
        }

        public bool CheckAreaForMine(
            Coordinates input,
            IEnumerable<Coordinates> mines,
            Limits limits)
        {
            return mines.Any(mine => 
                _coordinatesFactory.GetCoordinatesAround(limits)
                .Select(direction => direction.Check(input, mine))
                .Any(isMine => isMine));
        }
    }
}