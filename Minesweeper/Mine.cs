using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class Mine
    {
        public IEnumerable<Coordinates> GenerateMines(Limits limits, int numberOfMines)
        {
            var mines = new List<Coordinates>();

            for (var i = 1; i <= numberOfMines; i++)
            {
                var mine = GenerateMine(limits);
                mines.Add(mine);
            }

            return mines;
        }

        private static Coordinates GenerateMine(Limits limits)
        {
            var random = new Random();

            return new Coordinates
            {
                X = random.Next(1, limits.X - 1),
                Y = random.Next(1, limits.Y - 1)
            };
        }
    }
}