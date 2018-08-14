using System;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SudokuSolver.Console.Models;

namespace SudokuSolver.Console.Tests
{
    [TestFixture]
    public class SetOfSquaresTests
    {
        [Test]
        public void SetOfSquares_CreateNew_EmptySquareArray_ThrowsException()
        {
            var squares = new Square[] { };

            Assert.Throws<ArgumentException>(() =>
            {
                var setOfSquares = new SetOfSquares(squares);
            });
        }

        [Test]
        public void SetOfSquares_CreateNew_EmptyNineSquareArray_ThrowsException()
        {
            var squares = new Square[9];

            Assert.Throws<ArgumentException>(() =>
            {
                var setOfSquares = new SetOfSquares(squares);
            });
        }

        [Test]
        public void SetOfSquares_CreateNew_NonEmptyNineSquareArray_WithDuplicateNonZeroValues_ThrowsException(
            [Range(1, 9, 1)] int repeatedValue)
        {
            var squares = new[]
            {
                new Square(repeatedValue),
                new Square(repeatedValue),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square()
            };

            Assert.Throws<ArgumentException>(() =>
            {
                var setOfSquares = new SetOfSquares(squares);
            });
        }

        [Test]
        public void SetOfSquares_CreateNew_NonEmptyNineSquareArray_WithUniqueNonZeroValues_NoExceptionThrown()
        {
            var squares = new[]
            {
                new Square(1),
                new Square(2),
                new Square(3),
                new Square(4),
                new Square(5),
                new Square(6),
                new Square(7),
                new Square(8),
                new Square(9)
            };

            Assert.DoesNotThrow(() =>
            {
                var setOfSquares = new SetOfSquares(squares);
            });
        }

        [Test]
        public void SetOfSquares_TryChangeSquareValue_InvalidSquareIndex_ThrowsException(
            [Values(-1, 9)] int testIndex)
        {
            var squares = new[]
            {
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square()
            };

            var set = new SetOfSquares(squares);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var result = set.TryChangeSquareValue(testIndex, 0);
            });
        }

        [Test]
        public void SetOfSquares_TryChangeSquareValue_SquareIsFixed_ReturnsFalse(
            [Range(1, 9, 1)] int testValue)
        {
            var squares = new[]
            {
                new Square(1),
                new Square(2),
                new Square(3),
                new Square(4),
                new Square(5),
                new Square(6),
                new Square(7),
                new Square(8),
                new Square(9)
            };

            var set = new SetOfSquares(squares);

            const int testSquare = 0;
            var result = set.TryChangeSquareValue(testSquare, testValue);

            result.Should().BeFalse();
        }

        [Test]
        public void SetOfSquares_TryChangeSquareValue_SquareIsChangeable_ButNonPermittedValue_ThrowsException(
            [Values(-1, 10)] int testValue)
        {
            var squares = new[]
            {
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square()
            };

            var set = new SetOfSquares(squares);

            const int testSquare = 0;

            Assert.Throws<ArgumentException>(() =>
            {
                var result = set.TryChangeSquareValue(testSquare, testValue);
            });
        }

        [Test]
        public void SetOfSquares_TryChangeSquareValue_SquareIsChangeable_AndPermittedValue_ButValueAlreadyExistsInSet_ReturnsFalse(
            [Range(1, 9, 1)] int testValue)
        {
            var squares = new[]
            {
                new Square(),
                new Square(testValue),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square()
            };

            var set = new SetOfSquares(squares);

            const int testSquare = 0;
            var result = set.TryChangeSquareValue(testSquare, testValue);

            result.Should().BeFalse();
        }

        [Test]
        public void SetOfSquares_TryChangeSquareValue_SquareIsChangeable_AndPermittedValue_AndValueUniqueInSet_ReturnsTrue(
            [Range(1, 9, 1)] int testValue)
        {
            var squares = new[]
            {
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square(),
                new Square()
            };

            var set = new SetOfSquares(squares);

            const int testSquare = 0;
            var result = set.TryChangeSquareValue(testSquare, testValue);

            result.Should().BeTrue();
        }
    }
}
