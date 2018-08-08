using System;

namespace SudokuSolver.Console.Models
{
    public class Square
    {
        private const int EmptySquareValue = 0;
        private const int MinPermittedValue = 1;
        private const int MaxPermittedValue = 9;

        public bool ValueIsChangeable { get; }
        public int? Value { get; private set; }

        public Square(bool valueIsChangeable, int value = EmptySquareValue)
        {
            CheckValue(value);

            ValueIsChangeable = valueIsChangeable;
            Value = value;
        }

        public bool TryChangeValue(int value)
        {
            if (ValueIsChangeable)
            {
                CheckValue(value);
                Value = value;
                return true;
            }

            return false;
        }

        private static void CheckValue(int value)
        {
            if (value != EmptySquareValue && (value < MinPermittedValue || value > MaxPermittedValue))
            {
                throw new ArgumentException(
                    $"{value} is not a permitted value; should be in range: {MinPermittedValue} - {MaxPermittedValue}.");
            }
        }
    }
}
