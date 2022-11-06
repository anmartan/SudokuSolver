namespace SudokuSolver
{
    public struct Point
    {
        public int _row, _column;

        public Point(int row, int column)
        {
            _row = row;
            _column = column;
        }

        public static bool operator ==(Point a, Point b)
        {
            return a._row == b._row && a._column == b._column;
        }
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
    }
}