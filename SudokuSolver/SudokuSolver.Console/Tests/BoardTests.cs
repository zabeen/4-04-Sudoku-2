using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SudokuSolver.Console.Models;
using System;

namespace SudokuSolver.Console.Tests
{
    [TestFixture]
    public class BoardTests
    {
        private const int SquareArrayLength = 81;

        [Test]
        public void Board_CreateNew_EmptySquareArray_ThrowsException()
        {
            var squares = new Square[] { };

            Assert.Throws<ArgumentException>(() =>
            {
                var setOfSquares = new Board(squares);
            });
        }

        [Test]
        public void Board_CreateNew_EmptySquareArrayOfCorrectLength_ThrowsException()
        {
            var squares = new Square[SquareArrayLength];

            Assert.Throws<ArgumentException>(() =>
            {
                var setOfSquares = new Board(squares);
            });
        }

        [Test]
        public void Board_CreateNew_NonEmptySquareArrayOfCorrectLength_WithDuplicateNonZeroValuesInSameSquareSet_ThrowsException(
            [Range(1, 9, 1)] int repeatedValue)
        {
            var squares = new Square[SquareArrayLength];

            // these two square indexes are part of same 3x3 set and row set
            const int firstSquare = 0;
            const int secondSquare = 1;

            squares[firstSquare] = new Square(repeatedValue);
            squares[secondSquare] = new Square(repeatedValue);

            // rest of the squares are default
            for (var i = secondSquare + 1; i < squares.Length; i++)
            {
                squares[i] = new Square();
            }

            Assert.Throws<ArgumentException>(() =>
            {
                var setOfSquares = new Board(squares);
            });
        }

        [Test]
        public void Board_CreateNew_NonEmptySquareArrayOfCorrectLength_WithUniqueNonZeroValues_NoExceptionThrown()
        {
            var squares = new Square[SquareArrayLength];

            // these square indexes are part of same 3x3 & row/column set
            const int firstSquare = 0;
            const int secondSquare = 1;
            const int tenthSquare = 9;

            squares[firstSquare] = new Square(1);
            squares[secondSquare] = new Square(2);
            squares[tenthSquare] = new Square(3);

            // rest of the squares are default
            for (var i = secondSquare + 1; i < squares.Length; i++)
            {
                squares[i] = new Square();
            }

            Assert.DoesNotThrow(() =>
            {
                var setOfSquares = new Board(squares);
            });
        }

        [Test]
        public void Board_TryChangeSquareValue_InvalidSquareIndex_ThrowsException(
            [Values(-1, 81)] int testIndex)
        {
            var squares = new Square[SquareArrayLength];
            for (var i = 0; i < squares.Length; i++)
            {
                squares[i] = new Square();
            }

            var board = new Board(squares);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var result = board.TryChangeSquareValue(testIndex, 0);
            });
        }

        [Test]
        public void Board_TryChangeSquareValue_SquareIsFixed_ReturnsFalse(
            [Range(1, 9, 1)] int testValue)
        {
            var squares = new Square[SquareArrayLength];

            const int testSquare = 0;

            squares[testSquare] = new Square(1);

            // rest of squares are default
            for (var i = testSquare + 1; i < squares.Length; i++)
            {
                squares[i] = new Square();
            }

            var board = new Board(squares);

            var result = board.TryChangeSquareValue(testSquare, testValue);

            result.Should().BeFalse();
        }

        [Test]
        public void Board_TryChangeSquareValue_SquareIsChangeable_ButNonPermittedValue_ThrowsException(
            [Values(-1, 10)] int testValue)
        {
            var squares = new Square[SquareArrayLength];
            for (var i = 0; i < squares.Length; i++)
            {
                squares[i] = new Square();
            }

            var board = new Board(squares);

            const int testSquare = 0;

            Assert.Throws<ArgumentException>(() =>
            {
                var result = board.TryChangeSquareValue(testSquare, testValue);
            });
        }

        [Test]
        public void Board_TryChangeSquareValue_SquareIsChangeable_AndPermittedValue_ButValueAlreadyExistsInSquareSet_ReturnsFalse(
            [Range(1, 9, 1)] int testValue)
        {
            var squares = new Square[SquareArrayLength];

            // these two square indexes are part of same 3x3 set and row set
            const int firstSquare = 0;
            const int secondSquare = 1;

            squares[firstSquare] = new Square(testValue);

            // rest of squares are default
            for (var i = secondSquare; i < squares.Length; i++)
            {
                squares[i] = new Square();
            }

            var board = new Board(squares);

            var result = board.TryChangeSquareValue(secondSquare, testValue);

            result.Should().BeFalse();
        }

        [Test]
        public void Board_TryChangeSquareValue_SquareIsChangeable_AndPermittedValue_AndValueUniqueInSquareSet_ReturnsTrue(
            [Range(3, 9, 1)] int testValue)
        {
            var squares = new Square[SquareArrayLength];

            // these square indexes are part of same 3x3 & row/column set
            const int firstSquare = 0;
            const int secondSquare = 1;
            const int tenthSquare = 9;

            squares[firstSquare] = new Square(1);
            squares[secondSquare] = new Square(2);

            // rest of squares are default
            for (var i = secondSquare + 1; i < squares.Length; i++)
            {
                squares[i] = new Square();
            }

            var board = new Board(squares);
            var result = board.TryChangeSquareValue(tenthSquare, testValue);

            result.Should().BeTrue();
        }
    }
}
