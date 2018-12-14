using System.Collections.Generic;
using Minesweeper.CoordinatesAround;
using NSubstitute;
using NUnit.Framework;

namespace Minesweeper.Tests
{
    [TestFixture]
    public class MineTests
    {
        private Coordinates _coordinates;
        private Sweep _sweep;
        private Validate _validate;
        private Limits _limits;
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

        [TestCase("Field #A1", "B2")]
        public void InputDoesNotMatchMineCoordinates(string input, string mine)
        {
            // given

            // when
            var inputCoordinates = _coordinates.Get(input);
            var isValid = _validate.InputIsValid(input);
            var isMine = _sweep.IsMine(inputCoordinates, mine);

            // then
            Assert.That(isValid, Is.True);
            Assert.That(isMine, Is.False);
        }

        [TestCase("Field #B2", "B2")]
        public void InputMatchesMineCoordinates(string input, string mine)
        {
            // given

            // when
            var inputCoordinates = _coordinates.Get(input);
            var isValid = _validate.InputIsValid(input);
            var isMine = _sweep.IsMine(inputCoordinates, mine);

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

        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(10, 9)]
        public void CoordinatesAboveInputIsMine(int inputY, int mineY)
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = 3,
                Y = inputY
            };
            var mineCoordinates = new Coordinates
            {
                X = 3,
                Y = mineY
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new AboveCoordinates() });
            var mineAbove = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineAbove, Is.True);
        }

        [TestCase(1, 6)]
        [TestCase(4, 5)]
        [TestCase(10, 8)]
        public void CoordinatesAboveInputIsNotMine(int inputY, int mineY)
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = 3,
                Y = inputY
            };
            var mineCoordinates = new Coordinates
            {
                X = 3,
                Y = mineY
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new AboveCoordinates() });
            var mineAbove = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineAbove, Is.False);
        }

        [TestCase(3, 4)]
        [TestCase(4, 5)]
        [TestCase(9, 10)]
        public void CoordinatesBelowInputIsMine(int inputY, int mineY)
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = 3,
                Y = inputY
            };
            var mineCoordinates = new Coordinates
            {
                X = 3,
                Y = mineY
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new BelowCoordinates(_limits) });
            var mineBelow = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineBelow, Is.True);
        }

        [TestCase(3, 5)]
        [TestCase(4, 3)]
        [TestCase(10, 7)]
        public void CoordinatesBelowInputIsNotMine(int inputY, int mineY)
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = 3,
                Y = inputY
            };
            var mineCoordinates = new Coordinates
            {
                X = 3,
                Y = mineY
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new BelowCoordinates(_limits) });
            var mineBelow = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineBelow, Is.False);
        }

        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(10, 9)]
        public void CoordinatesLeftOfInputIsMine(int inputX, int mineX)
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = inputX,
                Y = 3
            };
            var mineCoordinates = new Coordinates
            {
                X = mineX,
                Y = 3
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new LeftCoordinates() });
            var mineLeft = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineLeft, Is.True);
        }

        [TestCase(3, 4)]
        [TestCase(4, 8)]
        [TestCase(9, 10)]
        public void CoordinatesLeftOfInputIsNotMine(int inputX, int mineX)
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = inputX,
                Y = 3
            };
            var mineCoordinates = new Coordinates
            {
                X = mineX,
                Y = 3
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new LeftCoordinates() });
            var mineLeft = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineLeft, Is.False);
        }

        [TestCase(3, 4)]
        [TestCase(4, 5)]
        [TestCase(9, 10)]
        public void CoordinatesRightOfInputIsMine(int inputX, int mineX)
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = inputX,
                Y = 3
            };
            var mineCoordinates = new Coordinates
            {
                X = mineX,
                Y = 3
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new RightCoordinates(_limits) });
            var mineRight = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineRight, Is.True);
        }

        [TestCase(1, 4)]
        [TestCase(3, 5)]
        [TestCase(10, 5)]
        public void CoordinatesRightOfInputIsNotMine(int inputX, int mineX)
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = inputX,
                Y = 3
            };
            var mineCoordinates = new Coordinates
            {
                X = mineX,
                Y = 3
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new RightCoordinates(_limits) });
            var mineRight = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineRight, Is.False);
        }
    }
}