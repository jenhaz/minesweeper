using System;
using System.Collections.Generic;

namespace Minesweeper
{
    class Program
    {
        private static readonly Sweep Sweep;
        private static readonly Validate Validate;
        private static readonly Coordinates Coordinates;
        private static readonly List<Coordinates> PreviousGuesses;
        private static readonly int Height;
        private static readonly int Width;
        private static readonly Limits Limits;

        private static bool _isAlive;
        private static bool _inputIsValid;

        private static readonly IEnumerable<Coordinates> Mines;

        static Program()
        {
            Sweep = new Sweep(new CoordinatesFactory());
            Validate = new Validate();
            Coordinates = new Coordinates();
            PreviousGuesses = new List<Coordinates>();

            Height = 11;
            Width = 11;

            Limits = new Limits { X = Width, Y = Height + 1 };

            _isAlive = true;
            _inputIsValid = true;

            var numberOfMines = 2;
            Mines = new Mine().GenerateMines(Limits, numberOfMines);
        }
        
        static void Main(string[] args)
        {
            PrintGrid();

            while (_isAlive)
            {
                var input = Console.ReadLine();
                Console.WriteLine("You guessed: " + input);

                _inputIsValid = IsValid(input);

                while (!_inputIsValid)
                {
                    TryAgain();
                    input = Console.ReadLine();
                    _inputIsValid = IsValid(input);
                }

                if (_inputIsValid)
                {
                    var coords = Coordinates.Get(input);
                    var isMine = Sweep.IsMine(coords, Mines);

                    if (isMine)
                    {
                        _isAlive = false;
                        LoseAndExit();
                    }
                    else
                    {
                        PreviousGuesses.Add(coords);
                        PrintGrid(PreviousGuesses, new int[Limits.X, Limits.Y]);
                    }
                }
            }
        }

        static void PrintGrid()
        {
            Console.Write("x A B C D E F G H I J");

            for (int row = 1; row < Width; row++)
            {
                Console.WriteLine();
                Console.Write(row + " ");

                for (int column = 1; column < Height; column++)
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

            for (var row = 1; row < Height; row++)
            {
                Console.WriteLine();
                Console.Write(row + " ");

                if (allCoordinates != null)
                {
                    foreach (var coordinates in allCoordinates)
                    {
                        var output = Sweep.CheckAreaForMine(coordinates, Mines, Limits)
                            ? 1
                            : 8;

                        grid[coordinates.X, coordinates.Y] = output;

                        var areaAround = Sweep.GetCoordinatesAroundInput(coordinates, Limits);

                        foreach (var area in areaAround)
                        {
                            grid[area.X, area.Y] = GetGridPlaceholder(area);
                        }
                    }
                }

                for (var column = 1; column < Width; column++)
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
            return Validate.InputIsValid(input) &&
                   Validate.InputIsWithinRange(input, Limits) &&
                   Coordinates.Get(input) != null;
        }

        private static void TryAgain()
        {
            Console.WriteLine("lol no TRY AGAIN");
            Console.WriteLine();
            PrintGrid(PreviousGuesses, new int[Limits.X, Limits.Y]);
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

            if (Sweep.IsMine(coordinates, Mines))
            {
                output = 9;
            }
            else if (Sweep.CheckAreaForMine(coordinates, Mines, Limits))
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