using System.Collections.Generic;

namespace SudokuSolver 
{
    public class SudokuCell 
    {
        private readonly int _region;                   // Region to which the cell belongs (boxes or irregular shapes)
        private readonly List<int> _possibleValues;     // Indexes at which the value is 'true' could be the value for the cell
        private int _currentValue;                      // Value that is 'written' on the cell, if any

        // Might change later
        private const int size = 9;
        public SudokuCell(int region, int value = 0) 
        {
            _region = region;
            _currentValue = value;
            _possibleValues = new List<int>();
            
            if (_currentValue != 0)
            {
                _possibleValues.Add(_currentValue);
            }
            
            else 
            {
                for (int i = 1; i < size; i++) 
                    _possibleValues.Add(i);
            }
        }
        
        public int GetRegion() { return _region; }
        public List<int> GetPossibleValues() { return _possibleValues; }

        private bool RemovePossibility(int number)
        {
            bool removed = _possibleValues.Remove(number);
            if (_possibleValues.Count == 1) _currentValue = _possibleValues[0];
            return removed;
        }
    }

}