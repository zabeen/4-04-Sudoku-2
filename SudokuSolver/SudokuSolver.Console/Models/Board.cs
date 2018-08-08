namespace SudokuSolver.Console.Models
{
    public class Board
    {
        private const int NumberOfThreeByThrees = 3;
        private const int NumberOfOneByNines = 9;

        private ThreeByThree[,] threeByThrees;
        private OneByNine[] rows;
        private OneByNine[] columns;

        public Board()
        {
            threeByThrees = new ThreeByThree[NumberOfThreeByThrees, NumberOfThreeByThrees];
            rows = new OneByNine[NumberOfOneByNines];
            columns = new OneByNine[NumberOfOneByNines];
        }
    }
}
