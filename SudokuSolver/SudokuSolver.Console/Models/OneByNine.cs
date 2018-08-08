namespace SudokuSolver.Console.Models
{
    public class OneByNine
    {
        private const int NumberOfSquares = 9;

        private readonly Square[] squares;

        public OneByNine()
        {
            squares = new Square[NumberOfSquares];
        }

        public Square GetSquare(int index)
        {
            return squares[index];
        }
    }
}
