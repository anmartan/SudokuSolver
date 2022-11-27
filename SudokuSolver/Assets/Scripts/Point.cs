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

        public static Point operator *(Point a, int k)
        {
            return new Point(a._row * k, a._column * k);
        }
        public static Point operator +(Point a, Point b)
        {
            return new Point(a._row + b._row, a._column + b._column);
        }
    }
}