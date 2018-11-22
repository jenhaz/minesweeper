using System.Linq;

namespace Minesweeper
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinates Get(string input)
        {
            var coordinates = input.Split('#').Last();
            var x = coordinates[0];

            if (!char.IsLetter(x) ||
                coordinates.Length > 3)
            {
                return null;
            }

            if (coordinates.Length == 2)
            {
                x = coordinates[0];
                var y = (coordinates[1] - '1') + 1;

                return Convert(x, y);
            }

            if (coordinates.Length == 3)
            {
                x = coordinates[0];
                var y = int.Parse(coordinates.Substring(1, 2));

                return Convert(x, y);
            }

            return null;
        }
        
        private Coordinates Convert(char xCoordinate, int yCoordinate)
        {
            return new Coordinates
            {
                X = xCoordinate % 32,
                Y = yCoordinate
            };
        }
    }
}
