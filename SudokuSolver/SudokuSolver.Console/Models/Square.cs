namespace SudokuSolver.Console.Models
{
    public class Square
    {
        public bool ValueIsChangeable { get; }
        public int Value { get; }

        public Square(bool valueIsChangeable, int value = 0)
        {
            ValueIsChangeable = valueIsChangeable;
            Value = value;
        }

        public bool TryChangeValue(int value)
        {
            return false;
        }
    }
}
