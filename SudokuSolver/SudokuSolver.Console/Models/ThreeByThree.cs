namespace SudokuSolver.Console.Models
{
    public class ThreeByThree
    {
        private const int NumberOfSquares = 3;

        private readonly Square[,] squares;

        public ThreeByThree()
        {
            squares = new Square[NumberOfSquares, NumberOfSquares];
        }

        public Square GetSquare(int x, int y)
        {
            return squares[x,y];
        }
    }
}
