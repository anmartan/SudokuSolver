using System.Collections.Generic;

namespace SudokuSolver
{
    /// <summary>
    /// A hidden single, or pinned digit, can be found when a number can only appear in one position in a certain row, column or region.
    /// This can be better found when doing pencil marks; if a number is only pencil marked in one position in a row, column or region, it's a hidden single.
    /// That number can immediately be placed.
    /// </summary>
    public class HiddenSingle : SolvingAlgorithm
    {
        public List<ValueFound> FindNumber(Sudoku sudoku)
        {
            int size = sudoku.GetSize();
            List<ValueFound> valuesFound = new List<ValueFound>();
            
            for (int i = 0; i < size; i++)
            {
                List<Point> rowSet = new List<Point>();
                List<Point> columnSet = new List<Point>();
                for (int j = 0; j < size; j++)
                {
                    rowSet.Add(new Point(i, j));
                    columnSet.Add(new Point(j, i));
                }
                
                valuesFound.AddRange(FindOccurrencesInSet(sudoku, rowSet));
                valuesFound.AddRange(FindOccurrencesInSet(sudoku, columnSet));
                valuesFound.AddRange(FindOccurrencesInSet(sudoku, sudoku.GetCellsInRegion(i)));
            }
            return valuesFound;
        }

        private List<ValueFound> FindOccurrencesInSet(Sudoku sudoku, List<Point> cells)
        {
            // Arrays to take note of the numbers that are repeated, and whether they are "fixed" into the grid or not
            bool[] isWritten = new bool[cells.Count];
            List<int>[] count = new List<int>[cells.Count];
            for (int i = 0; i < count.Length; i++)
                count[i] = new List<int>();

            // Checks every cell that forms the set
            for (int i = 0; i < cells.Count; i++)
            {
                List<int> possibleValues = sudoku.GetPossibleValuesInCell(cells[i]);
                
                // If the cell is filled, it is not taken into account afterwards
                if (sudoku.IsCellFilled(cells[i]))
                    isWritten[possibleValues[0] - 1] = true;
                else // The position at which it was found is saved
                {
                    for (int j = 0; j < possibleValues.Count; j++)
                        count[possibleValues[j] - 1].Add(i);
                }
            }
            
            // Checks which values only appear once, and those are the ones returned
            List<ValueFound> values = new List<ValueFound>();
            for (int i = 0; i < cells.Count; i++)
            {
                if (!isWritten[i] && count[i].Count == 1)
                {
                    values.Add(new ValueFound(i + 1, cells[count[i][0]]._row, cells[count[i][0]]._column));
                    isWritten[i] = true;
                }
            }

            return values;
        }
    }
}