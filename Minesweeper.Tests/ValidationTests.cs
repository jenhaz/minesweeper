using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace Minesweeper.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        private Validate _validate;
        private Limits _limits;
        private Coordinates _coordinates;
        private Sweep _sweep;
        private ICoordinatesFactory _coordinatesFactory;

        [SetUp]
        public void SetUp()
        {
            _coordinatesFactory = Substitute.For<ICoordinatesFactory>();
            _coordinates = new Coordinates();
            _limits = new Limits { X = 11, Y = 12 };
            _sweep = new Sweep(_coordinatesFactory);
            _validate = new Validate();
        }

        [TestCase("Field #A1", 1, 1)]
        [TestCase("Field #B2", 2, 2)]
        [TestCase("Field #C3", 3, 3)]
        [TestCase("Field #J10", 10, 10)]
        public void UserInputConvertsToCoordinate(string input, int expectedX, int expectedY)
        {
            // given

            // when
            var coordinates = _coordinates.Get(input);
            var isValid = _validate.InputIsValid(input);

            // then
            Assert.That(isValid, Is.True);
            Assert.That(coordinates.X, Is.EqualTo(expectedX));
            Assert.That(coordinates.Y, Is.EqualTo(expectedY));
        }

        [TestCase("Field #A1")]
        public void InputDoesNotMatchMineCoordinates(string input)
        {
            // given
            var mines = new List<Coordinates>
            {
                new Coordinates {X = 2, Y = 2}
            };

            // when
            var inputCoordinates = _coordinates.Get(input);
            var isValid = _validate.InputIsValid(input);
            var isMine = _sweep.IsMine(inputCoordinates, mines);

            // then
            Assert.That(isValid, Is.True);
            Assert.That(isMine, Is.False);
        }

        [TestCase("Field #B2")]
        public void InputMatchesMineCoordinates(string input)
        {
            // given
            var mines = new List<Coordinates>
            {
                new Coordinates {X = 2, Y = 2}
            };

            // when
            var inputCoordinates = _coordinates.Get(input);
            var isValid = _validate.InputIsValid(input);
            var isMine = _sweep.IsMine(inputCoordinates, mines);

            // then
            Assert.That(isValid, Is.True);
            Assert.That(isMine, Is.True);
        }

        [TestCase("Field #310")]
        [TestCase("lalalalalalala")]
        [TestCase("Field #lalalalalalala")]
        [TestCase("Field #31048141")]
        public void InvalidInput(string input)
        {
            // given

            // when
            var coordinates = _coordinates.Get(input);

            // then
            Assert.That(coordinates, Is.Null);
        }

        [TestCase("#v2")]
        [TestCase("#a22")]
        [TestCase("#x12")]
        [TestCase("#c0")]
        public void OutsideOfBounds(string input)
        {
            // given

            // when
            var isValid = _validate.InputIsWithinRange(input, _limits);

            // then
            Assert.That(isValid, Is.False);
        }

        [TestCase("#b2")]
        [TestCase("#c2")]
        [TestCase("#j10")]
        public void InsideOfBounds(string input)
        {
            // given

            // when
            var isValid = _validate.InputIsWithinRange(input, _limits);

            // then
            Assert.That(isValid, Is.True);
        }

        [TestCase(",..???@")]
        [TestCase("#''''''~~~")]
        [TestCase("hjsfkad#,,,..{")]
        public void InputHasWeirdChars(string input)
        {
            // given

            // when
            var isValid = _validate.InputIsValid(input);

            // then
            Assert.That(isValid, Is.False);
        }

        [TestCase("Field #b10")]
        [TestCase("#g5")]
        [TestCase("potato #h7")]
        [TestCase("j10")]
        public void InputDoesNotHaveWeirdChars(string input)
        {
            // given

            // when
            var isValid = _validate.InputIsValid(input);

            // then
            Assert.That(isValid, Is.True);
        }

        [TestCase(2, 2)]
        [TestCase(4, 4)]
        [TestCase(6, 3)]
        [TestCase(8, 10)]
        [TestCase(32, 34)]
        [TestCase(2, 42)]
        public void GenerateMinesWithinLimits(int xLimit, int yLimit)
        {
            // given
            var limits = new Limits
            {
                X = xLimit,
                Y = yLimit
            };

            // when
            var mines = new Mine().GenerateMines(limits, 1);

            // then
            Assert.That(mines.First().X, Is.Not.EqualTo(limits.X));
            Assert.That(mines.First().Y, Is.Not.EqualTo(limits.X));
        }
    }
}
