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

        public bool IsMine(Coordinates input, string mine)
        {
            var mineCoordinates = new Coordinates().Get(mine);

            return input.X == mineCoordinates.X && input.Y == mineCoordinates.Y;
        }

        public bool IsMine(Coordinates input, Coordinates mine)
        {
            return input.X == mine.X && input.Y == mine.Y;
        }
        
        public List<Coordinates> GetCoordinatesAroundInput(
            Coordinates input,
            Limits limits)
        {
            var result = new List<Coordinates>();

            var y = input.Y;
            var x = input.X;

            result.Add(new Coordinates
            {
                X = x,
                Y = y - 1 != 0 ? y - 1 : 0
            });

            result.Add(new Coordinates
            {
                X = x,
                Y = y + 1 != limits.Y ? y + 1 : y
            });

            result.Add(new Coordinates
            {
                X = x - 1 != 0 ? x - 1 : 0,
                Y = y
            });

            result.Add(new Coordinates
            {
                X = x + 1 != limits.X ? x + 1 : x,
                Y = y
            });

            return result;
        }
        
        public bool CheckAreaForMine(
            Coordinates inputCoordinates,
            Coordinates mineCoordinates,
            Limits limits)
        {
            return _coordinatesFactory.GetCoordinatesAround(limits)
                .Select(direction => direction.Check(inputCoordinates, mineCoordinates))
                .Any(isMine => isMine);
        }
    }
}