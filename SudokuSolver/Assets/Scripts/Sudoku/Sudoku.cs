namespace SudokuSolver 
{
    public class Sudoku 
    {
        private SudokuCell[][] _grid;
        
        public Sudoku()
        {
            _grid = new SudokuCell[9][];
            for (int i = 0; i < 9; i++)
            {
                _grid[i] = new SudokuCell[9];
                for (int j = 0; j < 9; j++)
                {
                    _grid[i][j] = new SudokuCell(GetRegion(j, i));
                } 
            }
        }

        public int GetRegion(int x, int y)
        {
            return ((x / 3) + 1) + ((y / 3) * 3);
        }
    }
}
/// 1 1 1  2 2 2  3 3 3     
/// 1 1 1  2 2 2  3 3 3
/// 1 1 1  2 2 2  3 3 3
/// 
/// 4 4 4  5 5 5  6 6 6
/// 4 4 4  5 5 5  6 6 6
/// 4 4 4  5 5 5  6 6 6
///
/// 7 7 7  8 8 8  9 9 9
/// 7 7 7  8 8 8  9 9 9
/// 7 7 7  8 8 8  9 9 9