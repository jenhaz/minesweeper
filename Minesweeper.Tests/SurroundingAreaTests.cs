using System.Collections.Generic;
using Minesweeper.CoordinatesAround;
using NSubstitute;
using NUnit.Framework;

namespace Minesweeper.Tests
{
    [TestFixture]
    public class SurroundingAreaTests
    {
        private Sweep _sweep;
        private Limits _limits;
        private ICoordinatesFactory _coordinatesFactory;

        [SetUp]
        public void SetUp()
        {
            _coordinatesFactory = Substitute.For<ICoordinatesFactory>();
            _limits = new Limits { X = 11, Y = 12 };
            _sweep = new Sweep(_coordinatesFactory);
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
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates {X = 3, Y = mineY}
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
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates {X = 3, Y = mineY}
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
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates {X = 3, Y = mineY}
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
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates { X = 3, Y = mineY }
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
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates { X = mineX, Y = 3 }
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
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates { X = mineX, Y = 3 }
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
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates { X = mineX, Y = 3 }
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
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates { X = mineX, Y = 3 }
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new RightCoordinates(_limits) });
            var mineRight = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineRight, Is.False);
        }

        [Test]
        public void CoordinatesAboveAndRightOfInputIsMine()
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = 3,
                Y = 3
            };
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates {X = 4, Y = 2}
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new AboveRightCoordinates(_limits) });
            var mineAboveRight = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineAboveRight, Is.True);
        }
        
        [Test]
        public void CoordinatesAboveAndRightOfInputIsNotMine()
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = 3,
                Y = 3
            };
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates {X = 3, Y = 2}
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new AboveRightCoordinates(_limits) });
            var mineAboveRight = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineAboveRight, Is.False);
        }

        [Test]
        public void CoordinatesAboveAndLeftOfInputIsMine()
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = 3,
                Y = 3
            };
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates {X = 2, Y = 2}
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new AboveLeftCoordinates() });
            var mineAboveLeft = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineAboveLeft, Is.True);
        }

        [Test]
        public void CoordinatesAboveAndLeftOfInputIsNotMine()
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = 3,
                Y = 3
            };
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates {X = 3, Y = 2}
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new AboveLeftCoordinates() });
            var mineAboveLeft = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineAboveLeft, Is.False);
        }

        [Test]
        public void CoordinatesBelowAndLeftOfInputIsMine()
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = 3,
                Y = 3
            };
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates {X = 2, Y = 4}
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new BelowLeftCoordinates(_limits) });
            var mineBelowLeft = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineBelowLeft, Is.True);
        }

        [Test]
        public void CoordinatesBelowAndLeftOfInputIsNotMine()
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = 3,
                Y = 3
            };
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates {X = 3, Y = 4}
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new BelowLeftCoordinates(_limits) });
            var mineBelowLeft = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineBelowLeft, Is.False);
        }

        [Test]
        public void CoordinatesBelowAndRightOfInputIsMine()
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = 3,
                Y = 3
            };
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates {X = 4, Y = 4}
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new BelowRightCoordinates(_limits) });
            var mineBelowLeft = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineBelowLeft, Is.True);
        }

        [Test]
        public void CoordinatesBelowAndRightOfInputIsNotMine()
        {
            // given
            var inputCoordinates = new Coordinates
            {
                X = 3,
                Y = 3
            };
            var mineCoordinates = new List<Coordinates>
            {
                new Coordinates {X = 3, Y = 4}
            };

            // when
            _coordinatesFactory.GetCoordinatesAround(_limits).Returns(new List<ICoordinatesAround> { new BelowRightCoordinates(_limits) });
            var mineBelowLeft = _sweep.CheckAreaForMine(inputCoordinates, mineCoordinates, _limits);

            // then
            Assert.That(mineBelowLeft, Is.False);
        }
    }
}