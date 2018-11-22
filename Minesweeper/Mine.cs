using System;

namespace Minesweeper
{
    public class Mine
    {
        public Coordinates GenerateMine(int xLimit, int yLimit)
        {
            var x = new Random().Next(1, xLimit);
            var y = new Random().Next(1, yLimit);

            return new Coordinates
            {
                X = x,
                Y = y
            };
        }
    }
}