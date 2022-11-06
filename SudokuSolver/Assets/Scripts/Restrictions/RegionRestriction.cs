using System.Collections.Generic;

namespace SudokuSolver
{
    public class RegionRestriction: Restriction
    {
        public void RemoveMarks(Sudoku sudoku, Point position, int mark)
        {
            List<Point> region = sudoku.GetCellsInRegion(position);
            for (int i = 0; i < region.Count; i++)
            {
                Point point = region[i];
                if (point != position)
                {
                    //Debug.Log("Removed region mark: " + mark + "at point (" + point._row + ", " + point._column + ")");
                    sudoku.RemoveMark(point, mark);
                }
            }
        }
    }
}