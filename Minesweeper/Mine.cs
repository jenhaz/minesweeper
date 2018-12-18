using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    public class Mine
    {
        public IEnumerable<Coordinates> GenerateMines(Limits limits, int numberOfMines)
        {
            var mines = new List<Coordinates>();

            for (var i = 1; i <= numberOfMines; i++)
            {
                Coordinates mine = null;
                while (mine == null)
                {
                    var randomMine = GenerateMine(limits);
                    if (!mines.Any(x => x.X == randomMine.X && x.Y == randomMine.Y))
                    {
                        mine = randomMine;
                    }
                }

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