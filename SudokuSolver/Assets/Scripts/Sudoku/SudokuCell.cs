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
        private readonly List<int> _pencilMarks;    // Indexes at which the value is 'true' could be the value for the cell
        private int _currentValue;                  // Value that is 'written' on the cell, if any

        // TODO Might change later
        private const int size = 9;

        public SudokuCell()
        {
            _pencilMarks = new List<int>();
            for (int i = 1; i <= size; i++) 
                _pencilMarks.Add(i);
        }
        public void Init(int value = 0)
        {
            if(value != 0) SetValue(value);
            else _currentValue = value;
        }
        
        public int GetCurrentValue() { return _currentValue; }
        public List<int> GetPossibleValues() { return _pencilMarks; }

        public void SetValue(int number)
        {
            _pencilMarks.Clear();
            _pencilMarks.Add(number);
            _currentValue = number;
        }
        public bool RemovePencilMark(int number)
        {
            return _pencilMarks.Remove(number);
        }
    }

}