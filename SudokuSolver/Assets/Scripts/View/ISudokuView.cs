namespace SudokuSolver.Views
{
    public interface ISudokuView
    {
        public void SetValue(Point position, int number);

        public void RemovePencilMark(Point position, int mark);
    }
}