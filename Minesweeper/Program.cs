using System;
using System.Collections.Generic;
using Minesweeper.CoordinatesAround;

namespace Minesweeper
{
    public class Program
    {
        private static readonly Grid Grid;
        private static readonly Sweep Sweep;
        private static readonly Validate Validate;
        private static readonly Coordinates Coordinates;
        private static readonly List<Coordinates> PreviousGuesses;
        private static readonly Limits Limits;

        private static bool _isAlive;
        private static bool _inputIsValid;

        private static readonly IEnumerable<Coordinates> Mines;

        static Program()
        {
            const int height = 11;
            const int width = 11;
            const int numberOfMines = 2;

            Sweep = new Sweep(new CoordinatesAroundAroundFactory());
            Coordinates = new Coordinates();
            PreviousGuesses = new List<Coordinates>();

            Limits = new Limits { X = width, Y = height };
            Validate = new Validate(Coordinates, Limits);
            Mines = new Mine().GenerateMines(Limits, numberOfMines);
            Grid = new Grid(height, width, Limits, Sweep, Mines);

            _isAlive = true;
            _inputIsValid = true;
        }

        private static void Main(string[] args)
        {
            Grid.Print();

            while (_isAlive)
            {
                var input = Console.ReadLine();
                Console.WriteLine("You guessed: " + input);

                _inputIsValid = Validate.IsValid(input);

                while (!_inputIsValid)
                {
                    TryAgain();
                    input = Console.ReadLine();
                    _inputIsValid = Validate.IsValid(input);
                }

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
                    Grid.Print(PreviousGuesses, new int[Limits.X, Limits.Y]);
                }
            }
        }

        private static void TryAgain()
        {
            Console.WriteLine("Invalid input - please try again!");
            Console.WriteLine();
            Grid.Print(PreviousGuesses, new int[Limits.X, Limits.Y]);
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
    }
}