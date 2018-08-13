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
        public void SetOfSquares_CreateNew_PassInEmptySquareArray_ThrowsException()
        {
            var squares = new Square[] { };

            Assert.Throws<ArgumentException>(() =>
            {
                var setOfSquares = new SetOfSquares(squares);
            });
        }

        [Test]
        public void SetOfSquares_CreateNew_PassInEmptyNineSquareArray_ThrowsException()
        {
            var squares = new Square[9];

            Assert.Throws<ArgumentException>(() =>
            {
                var setOfSquares = new SetOfSquares(squares);
            });
        }

        [Test]
        public void SetOfSquares_CreateNew_PassInNonEmptyNineSquareArray_WithDuplicateNonZeroValues_ThrowsException(
            [Range(1, 9, 1)] int repeatedValue)
        {
            var squares = new[]
            {
                new Square(true, repeatedValue),
                new Square(true, repeatedValue),
                new Square(true),
                new Square(true),
                new Square(true),
                new Square(true),
                new Square(true),
                new Square(true),
                new Square(true)
            };

            Assert.Throws<ArgumentException>(() =>
            {
                var setOfSquares = new SetOfSquares(squares);
            });
        }

        [Test]
        public void SetOfSquares_CreateNew_PassInNonEmptyNineSquareArray_WithUniqueNonZeroValues_NoExceptionThrown()
        {
            var squares = new[]
            {
                new Square(true, 1),
                new Square(true, 2),
                new Square(true, 3),
                new Square(true, 4),
                new Square(true, 5),
                new Square(true, 6),
                new Square(true, 7),
                new Square(true, 8),
                new Square(true, 9)
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
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false)
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
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false)
            };

            var set = new SetOfSquares(squares);
            var result = set.TryChangeSquareValue(0, testValue);

            result.Should().BeFalse();
        }

        [Test]
        public void SetOfSquares_TryChangeSquareValue_ChangeableSquare_NonpermittedValue_ThrowsException(
            [Values(-1, 10)] int testValue)
        {
            var squares = new[]
            {
                new Square(true),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false)
            };

            var set = new SetOfSquares(squares);

            Assert.Throws<ArgumentException>(() =>
            {
                var result = set.TryChangeSquareValue(0, testValue);
            });
        }

        [Test]
        public void SetOfSquares_TryChangeSquareValue_ChangeableSquare_PermittedValue_ValueAlreadyExistsInSet_ReturnsFalse(
            [Range(1, 9, 1)] int testValue)
        {
            var squares = new[]
            {
                new Square(true),
                new Square(false, testValue),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false)
            };

            var set = new SetOfSquares(squares);
            var result = set.TryChangeSquareValue(0, testValue);

            result.Should().BeFalse();
        }

        [Test]
        public void SetOfSquares_TryChangeSquareValue_ChangeableSquare_PermittedValue_ValueUniqueInSet_ReturnsTrue(
            [Range(1, 9, 1)] int testValue)
        {
            var squares = new[]
            {
                new Square(true),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false),
                new Square(false)
            };

            var set = new SetOfSquares(squares);
            var result = set.TryChangeSquareValue(0, testValue);

            result.Should().BeTrue();
        }
    }
}
