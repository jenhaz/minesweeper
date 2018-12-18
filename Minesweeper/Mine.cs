using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class Mine
    {
        public Coordinates GenerateMine(Limits limits)
        {
            var x = new Random().Next(1, limits.X);
            var y = new Random().Next(1, limits.Y);

            return new Coordinates
            {
                X = x,
                Y = y
            };
        }

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
    }
}