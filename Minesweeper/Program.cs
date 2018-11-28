using System;
using System.Collections.Generic;

namespace Minesweeper
{
    class Program
    {
        private static readonly Sweep _sweep = new Sweep();
        private static readonly Validate _validate = new Validate();
        private static readonly Coordinates _coordinates = new Coordinates();
        private static readonly List<Coordinates> previousGuesses = new List<Coordinates>();

        private static readonly int height = 11;
        private static readonly int width = 11;
        
        private static readonly Limits limits = new Limits { X = width, Y = height + 1};

        private static bool isAlive = true;
        private static bool inputIsValid = true;

        private static readonly Coordinates mine = new Mine().GenerateMine(limits.X, limits.Y);

        static void Main(string[] args)
        {
            PrintGrid();

            while (isAlive)
            {
                var input = Console.ReadLine();
                Console.WriteLine("You guessed: " + input);

                inputIsValid = IsValid(input);

                while (!inputIsValid)
                {
                    TryAgain();
                    input = Console.ReadLine();
                    inputIsValid = IsValid(input);
                }

                if (inputIsValid)
                {
                    var coords = _coordinates.Get(input);
                    var isMine = _sweep.IsMine(coords, mine);

                    if (isMine)
                    {
                        isAlive = false;
                        LoseAndExit();
                    }
                    else
                    {
                        previousGuesses.Add(coords);
                        PrintGrid(previousGuesses, new int[limits.X, limits.Y]);
                    }
                }
            }
        }

        static void PrintGrid()
        {
            Console.Write("x A B C D E F G H I J");

            for (int row = 1; row < width; row++)
            {
                Console.WriteLine();
                Console.Write(row + " ");

                for (int column = 1; column < height; column++)
                {
                    Console.Write(". ");
                }
            }

            Console.WriteLine();
            Console.WriteLine("What's your guess?");
        }

        static void PrintGrid(List<Coordinates> allCoordinates, int[,] grid)
        {
            Console.Write("x A B C D E F G H I J");

            for (var row = 1; row < height; row++)
            {
                Console.WriteLine();
                Console.Write(row + " ");

                if (allCoordinates != null)
                {
                    foreach (var coordinates in allCoordinates)
                    {
                        var output = _sweep.CheckAreaForMine(coordinates, mine, limits) 
                            ? 1 
                            : 8;

                        grid[coordinates.X, coordinates.Y] = output;

                        var areaAround = _sweep.GetCoordinatesAroundInput(coordinates, limits);

                        foreach (var area in areaAround)
                        {
                            grid[area.X, area.Y] = GetGridPlaceholder(area);
                        }
                    }
                }

                for (var column = 1; column < width; column++)
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

        private static bool IsValid(string input)
        {
            return _validate.InputIsValid(input) &&
                   _validate.InputIsWithinRange(input, limits) &&
                   _coordinates.Get(input) != null;
        }

        private static void TryAgain()
        {
            Console.WriteLine("lol no TRY AGAIN");
            Console.WriteLine();
            PrintGrid(previousGuesses, new int[limits.X, limits.Y]);
        }

        private static void LoseAndExit()
        {
            Console.WriteLine("*****************************");
            Console.WriteLine("BOOOOOOOOOOOOOOOOOOOOOOOM!!!!");
            Console.WriteLine("*****************************");
            Console.WriteLine("Sorry, you dead.");
            Console.WriteLine("Say goodbye to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private static int GetGridPlaceholder(Coordinates coordinates)
        {
            int output;

            if (_sweep.IsMine(coordinates, mine))
            {
                output = 9;
            }
            else if (_sweep.CheckAreaForMine(coordinates, mine, limits))
            {
                output = 1;
            }
            else
            {
                output = 8;
            }

            return output;
        }
    }
}