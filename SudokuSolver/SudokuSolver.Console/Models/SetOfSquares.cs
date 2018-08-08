using System;
using System.Linq;

namespace SudokuSolver.Console.Models
{
    public class SetOfSquares
    {
        private const int NumberOfSquares = 9;

        private readonly Square[] squares;

        public SetOfSquares(Square[] squares)
        {
            if (squares.Length != NumberOfSquares)
            {
                throw new ArgumentException(
                    $"Length of {nameof(squares)} was {squares.Length}; should be {NumberOfSquares}.");
            }

            for (var i = 0; i < squares.Length; i++)
            {
                if (squares[i] == null)
                {
                    throw new ArgumentException(
                        $"{nameof(squares)} is null at index {i}.");
                }
            }

            if (squares
                .Where(s => !s.IsEmpty())
                .GroupBy(s => s.Value)
                .Any(group => group.Count() > 1))
            {
                throw new ArgumentException(
                    $"{nameof(squares)} contains duplicate values.");
            }

            this.squares = squares;
        }

        public bool TryChangeSquareValue(int index, int value)
        {
            if (index < 0 || index >= NumberOfSquares)
            {
                throw new ArgumentException(
                    $"Index is out of bounds; should be between 0 and {NumberOfSquares-1}");
            }

            if (squares.Any(s => s.Value == value))
            {
                return false;
            }

            var square = squares[index];
            return square.TryChangeValue(value);
        }
    }
}