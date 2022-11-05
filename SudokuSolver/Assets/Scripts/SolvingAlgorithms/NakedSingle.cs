using System.Collections.Generic;
using SudokuSolver;

namespace SudokuSolver
{
    /// <summary>
    /// A naked single, forced digit or sole candidate, can be found when a cell has an unique possible value.
    /// This can be better found when doing pencil marks; if a cell can only have one pencil-marked number it's a naked single.
    /// That number can immediately be placed.
    /// </summary>
    public class NakedSingle : SolvingAlgorithm
    {
        private int _valueFound;
        
        public ValueFound FindNumber(Sudoku sudoku)
        {
            for (int i = 0; i < sudoku.GetSize(); i++)
            {
                for (int j = 0; j < sudoku.GetSize(); j++)
                {
                    Point point = new Point(i, j);
                    if (!sudoku.IsCellFilled(point))
                    {
                        List<int> possibleValues = sudoku.GetPossibleValuesInCell(point);
                        if (possibleValues.Count == 1)
                            return new ValueFound(possibleValues[0], i, j);
                    }
                }
            }

            return new ValueFound();
        }
    }
}