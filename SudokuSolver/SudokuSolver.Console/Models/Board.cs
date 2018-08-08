namespace SudokuSolver.Console.Models
{
    public class Board
    {
        private const int NumberOfSets = 9;

        private SetOfSquares[] threeByThrees;
        private SetOfSquares[] rows;
        private SetOfSquares[] columns;

        public Board()
        {
            threeByThrees = new SetOfSquares[NumberOfSets];
            rows = new SetOfSquares[NumberOfSets];
            columns = new SetOfSquares[NumberOfSets];
        }
    }
}
