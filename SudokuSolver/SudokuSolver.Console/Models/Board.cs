namespace SudokuSolver.Console.Models
{
    public class Board
    {
        private const int NumberOfSquares = 81;
        private const int NumberOfSets = 9;

        private Square[] squares;
        private SetOfSquares[] threeByThrees;
        private SetOfSquares[] rows;
        private SetOfSquares[] columns;

        public Board(Square[] squares)
        {
            this.squares = new Square[NumberOfSquares];
            threeByThrees = new SetOfSquares[NumberOfSets];
            rows = new SetOfSquares[NumberOfSets];
            columns = new SetOfSquares[NumberOfSets];
        }

        public bool TryChangeSquareValue(int index, int value)
        {
            return false;
        }
    }
}
