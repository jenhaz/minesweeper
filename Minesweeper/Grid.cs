using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class Grid
    {
        private static int _height;
        private static int _width;
        private static Limits _limits;
        private readonly Sweep _sweep;
        private static IEnumerable<Coordinates> _mines;

        public Grid(
            int height, 
            int width, 
            Limits limits,
            Sweep sweep, 
            IEnumerable<Coordinates> mines)
        {
            _height = height;
            _width = width;
            _limits = limits;
            _sweep = sweep;
            _mines = mines;
        }

        public void Print()
        {
            Console.Write(GenerateXAxis(_limits.X));

            for (var row = 1; row < _width; row++)
            {
                Console.WriteLine();
                Console.Write(row + " ");

                for (var column = 1; column < _height; column++)
                {
                    Console.Write(". ");
                }
            }

            Console.WriteLine();
            Console.WriteLine("What's your guess?");
        }

        public void Print(IReadOnlyCollection<Coordinates> allCoordinates, int[,] grid)
        {
            Console.Write(GenerateXAxis(_limits.X));

            for (var row = 1; row < _height; row++)
            {
                Console.WriteLine();
                Console.Write(row + " ");

                if (allCoordinates != null)
                {
                    foreach (var coordinates in allCoordinates)
                    {
                        var output = _sweep.CheckAreaForMine(coordinates, _mines, _limits)
                            ? 1
                            : 8;

                        grid[coordinates.X, coordinates.Y] = output;

                        var areaAround = _sweep.GetCoordinatesAroundInput(coordinates, _limits);

                        foreach (var area in areaAround)
                        {
                            grid[area.X, area.Y] = GetGridPlaceholder(area);
                        }
                    }
                }

                for (var column = 1; column < _width; column++)
                {
                    if (grid[column, row] == 1)
                    {
                        Console.Write(grid[column, row] + " ");
                    }
                    else if (grid[column, row] == 8)
                    {
                        Console.Write("x ");
                    }
                    else if (grid[column, row] == 9)
                    {
                        Console.Write(". ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("What's your guess?");
        }

        private int GetGridPlaceholder(Coordinates coordinates)
        {
            int output;

            if (new Mine().IsMine(coordinates, _mines))
            {
                output = 9;
            }
            else if (_sweep.CheckAreaForMine(coordinates, _mines, _limits))
            {
                output = 1;
            }
            else
            {
                output = 8;
            }

            return output;
        }

        private static string GenerateXAxis(int xLimit)
        {
            var xAxis = string.Empty;

            for (var i = 0; i < xLimit - 1; i++)
            {
                xAxis += $"{Convert.ToChar(i + 65).ToString()} ";
            }
            return $"x {xAxis}";
        }
    }
}