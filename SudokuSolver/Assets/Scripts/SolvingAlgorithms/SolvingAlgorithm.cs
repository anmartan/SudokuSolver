namespace SudokuSolver
{
    /// <summary>
    /// This interface offers a way to use different algorithms in a simple way. Inheritors may implement their own way of "finding numbers", that is, searching for a cell
    /// that can be filled with the current sudoku state. This must return a //TODO may change its name // ValueFound
    /// Which indicates the position at which the value was found and the value that must be set to.
    /// It has a flag to indicate whether the value is valid or not (instead of returning a null value).
    /// </summary>
    public interface SolvingAlgorithm
    {
        public ValueFound FindNumber(Sudoku sudoku);
    }

    public struct ValueFound
    {
        public readonly bool _valid;
        public readonly int _number;
        public readonly Point _point;

        public ValueFound(int number, int row, int column)
        {
            _valid = true;
            _number = number;
            _point = new Point(row, column);
        }
    }
}
