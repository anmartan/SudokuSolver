using System.Collections.Generic;

namespace SudokuSolver 
{
    /// <summary>
    /// Logic class that represents each of the cells that form a sudoku; each of the spaces where a number must fit in.
    /// It has information about its possible values (pencil marks, as they are called), the region it belongs to
    /// (in case there are non-regular shapes in the future), and the value it holds at the moment (0 if it is empty). This information
    /// may seem redundant if this is just a solver, but I'll keep it in case there is user interaction in the future, which may require big changes though.
    ///
    /// This class doesn't have much logic: it's composed of getters and setters as it doesn't make much sense that a cell takes decisions by itself.
    /// </summary>
    public class SudokuCell 
    {
        private readonly int _region;                   // Region to which the cell belongs (boxes or irregular shapes)
        private readonly List<int> _pencilMarks;        // Indexes at which the value is 'true' could be the value for the cell
        private int _currentValue;                      // Value that is 'written' on the cell, if any

        // Might change later
        private const int size = 9;
        public SudokuCell(int region, int value = 0) 
        {
            _region = region;
            _currentValue = value;
            _pencilMarks = new List<int>();
            
            if (_currentValue != 0)
            {
                _pencilMarks.Add(_currentValue);
            }
            
            else 
            {
                for (int i = 1; i <= size; i++) 
                    _pencilMarks.Add(i);
            }
        }
        
        public int GetCurrentValue() { return _currentValue; }
        public int GetRegion() { return _region; }
        public List<int> GetPossibleValues() { return _pencilMarks; }

        public void SetValue(int number)
        {
            _currentValue = number;
        }
        public bool RemovePencilMark(int number)
        {
            return _pencilMarks.Remove(number);
        }
    }

}