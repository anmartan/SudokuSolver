namespace SudokuSolver
{
    public interface Restriction
    {
        public void RemoveMarks(Sudoku sudoku, Point position, int mark);
    }
}