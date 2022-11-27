namespace SudokuSolver.Model
{
    public class RowRestriction: Restriction
    {
        public void RemoveMarks(Sudoku sudoku, Point position, int mark)
        {
            for(int i = 0; i < sudoku.GetSize(); i++)
            {
                Point point = new Point(position._row, i);
                if (point != position)
                {
                    //Debug.Log("Removed row mark: " + mark + "at point (" + point._row + ", " + point._column + ")");
                    sudoku.RemoveMark(point, mark);
                }
            }
        }
    }
}