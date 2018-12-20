using System.Collections.Generic;
using System.Linq;
using Minesweeper.CoordinatesAround;

namespace Minesweeper
{
    public class Sweep
    {
        private readonly ICoordinatesAroundFactory _coordinatesAroundFactory;

        public Sweep(ICoordinatesAroundFactory coordinatesAroundFactory)
        {
            _coordinatesAroundFactory = coordinatesAroundFactory;
        }

        public bool IsMine(Coordinates input, IEnumerable<Coordinates> mines)
        {
            return mines.Any(mine => input.X == mine.X && input.Y == mine.Y);
        }

        public IEnumerable<Coordinates> GetCoordinatesAroundInput(
            Coordinates input,
            Limits limits)
        {
            return _coordinatesAroundFactory.GetCoordinatesAround(limits)
                .Select(direction => direction.Get(input))
                .ToList();
        }

        public bool CheckAreaForMine(
            Coordinates input,
            IEnumerable<Coordinates> mines,
            Limits limits)
        {
            return mines.Any(mine => 
                _coordinatesAroundFactory.GetCoordinatesAround(limits)
                .Select(direction => direction.Check(input, mine))
                .Any(isMine => isMine));
        }
    }
}