using System.Linq;

namespace Minesweeper
{
    public class Validate
    {
        public bool InputIsValid(string input)
        {
            var coordinates = input.Split('#').Last();

            if (coordinates.Any(c => !char.IsLetterOrDigit(c)))
            {
                return false;
            }

            return true;
        }

        public bool InputIsWithinRange(string input, Limits limits)
        {
            var coordinates = new Coordinates().Get(input);

            if (coordinates?.X <= 0 || 
                coordinates?.Y <= 0 || 
                coordinates?.X >= limits.X || 
                coordinates?.Y >= limits.Y)
            {
                return false;
            }

            return true;
        }
    }
}