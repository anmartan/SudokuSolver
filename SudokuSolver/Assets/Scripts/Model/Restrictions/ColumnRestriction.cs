namespace SudokuSolver.Model
{
    public class ColumnRestriction: Restriction
    {
        public void RemoveMarks(Sudoku sudoku, Point position, int mark)
        {
            for(int i = 0; i < sudoku.GetSize(); i++)
            {
                Point point = new Point(i, position._column);
                if (point != position)
                {
                    //Debug.Log("Removed column mark: " + mark + "at point (" + point._row + ", " + point._column + ")");
                    sudoku.RemoveMark(point, mark);
                }
            }
        }
    }
}