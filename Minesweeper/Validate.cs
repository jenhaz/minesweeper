using System.Linq;

namespace Minesweeper
{
    public class Validate
    {
        private readonly Coordinates _coordinates;
        private readonly Limits _limits;

        public Validate(
            Coordinates coordinates, 
            Limits limits)
        {
            _coordinates = coordinates;
            _limits = limits;
        }

        public bool IsValid(string input)
        {
            return !string.IsNullOrEmpty(input) &&
                   InputIsValid(input) &&
                   InputIsWithinRange(input, _limits) &&
                   _coordinates.Get(input) != null;
        }

        public bool InputIsValid(string input)
        {
            var coordinates = input.Split('#').Last();

            return coordinates.All(char.IsLetterOrDigit);
        }

        public bool InputIsWithinRange(string input, Limits limits)
        {
            var coordinates = _coordinates.Get(input);

            return !(coordinates?.X <= 0) 
                   && !(coordinates?.Y <= 0) 
                   && !(coordinates?.X >= limits.X) 
                   && !(coordinates?.Y >= limits.Y);
        }
    }
}