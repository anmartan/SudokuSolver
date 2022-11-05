using System.Collections.Generic;
using System.IO;

namespace SudokuSolver 
{
    
    /// <summary>
    /// Logic class that represents the sudoku grid, with its numbers and regions.
    /// This may also serve as a base class if there are sudoku variants in a future, like killer cages, arrows, renban lines, etc.
    /// This class takes care of communicating between the solver and the sudoku cells, and keeps the grid information updated.
    ///
    /// TODO In a near future, the grid will be read from a file instead of being hardcoded.
    /// </summary>
    public class Sudoku 
    {
        private SudokuCell[][] _grid;
        private Dictionary<int, List<Point>> _regions;
        private const int _SIZE_ = 9;
        
        public Sudoku()
        {
            _grid = new SudokuCell[_SIZE_][];
            _regions = new Dictionary<int, List<Point>>(_SIZE_);
            for (int i = 0; i < _SIZE_; i++)
            {
                _regions[i] = new List<Point>();
            }
            Initialize();
            
        }
        
        //TODO Change to read from file
        private void Initialize()
        {
            StreamWriter file = new StreamWriter("./prueba.txt");
            for (int i = 0; i < _SIZE_; i++)
            {
                _grid[i] = new SudokuCell[_SIZE_];
                for (int j = 0; j < _SIZE_; j++)
                {
                    int region = GetRegion(j, i);
                    _grid[i][j] = new SudokuCell(region, nakedSingles[i, j]);
                    _regions[region].Add(new Point(i, j));
                    file.Write(region + " ");
                } 
                file.WriteLine();
            }
            file.Close();
        }
        
        // TODO Auxiliary, remove 
        public int GetRegion(int x, int y)
        {
            return ((x / 3)) + ((y/3) * 3);
        }

        public int GetSize() { return _SIZE_; }

        public List<int> GetPossibleValuesInCell(Point point)
        {
            return _grid[point._row][point._column].GetPossibleValues();
        }

        public bool IsCellFilled(Point point)
        {
            return _grid[point._row][point._column].GetCurrentValue() != 0;
        }

        public void FillCell(Point point, int number)
        {
            _grid[point._row][point._column].SetValue(number);
            _regions[_grid[point._row][point._column].GetRegion()].Add(point);
        }
        

        public void RemovePencilMarks()
        {
            for (int i = 0; i < _SIZE_; i++)
            {
                for (int j = 0; j < _SIZE_; j++)
                {
                    List<int> pencilMarks = _grid[i][j].GetPossibleValues();

                    int k = 0;
                    while(k < pencilMarks.Count)
                    {
                        int mark = pencilMarks[k];
                        if (IsNumberInRow(mark, i) ||
                            IsNumberInColumn(mark, j) ||
                            IsNumberInRegion(mark, _grid[i][j].GetRegion()))
                        
                            _grid[i][j].RemovePencilMark(mark);
                        
                        else k++;
                    }
                } 
            }
        }

        private bool IsNumberInRow(int number, int row)
        {
            for(int i = 0; i < _SIZE_; i++)
            {
                if (_grid[row][i].GetCurrentValue() == number)
                    return true;
            }

            return false;
        }

        private bool IsNumberInColumn(int number, int column)
        {
            for(int i = 0; i < _SIZE_; i++)
            {
                if (_grid[i][column].GetCurrentValue() == number)
                    return true;
            }

            return false;
        }

        private bool IsNumberInRegion(int number, int region)
        {
            for (int i = 0; i < _regions[region].Count; i++)
            {
                Point point = _regions[region][i];
                if (_grid[point._row][point._column].GetCurrentValue() == number)
                    return true;
            }
            return false;
        }

        private static readonly int[,] nakedSingles = new int[,]
        {
            {0,0,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {7,0,0,0,0,0,0,0,0},
            {8,0,0,0,0,0,1,2,6},
            {3,4,6,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,4,0,0,0,0,0,0},
            {0,0,9,0,0,0,0,0,0},

        };
        private static readonly int[,] hiddenSingles = new int[,]
        {
            {0,0,2,1,9,3,0,0,0},
            {0,0,0,0,0,7,0,0,0},
            {7,0,0,0,4,0,0,1,9},
            {8,0,3,0,0,0,6,0,0},
            {0,4,5,0,0,0,2,3,0},
            {0,0,7,0,0,0,5,0,4},
            {3,7,0,0,8,0,0,0,6},
            {0,0,0,6,0,0,0,0,0},
            {0,0,0,5,3,4,1,0,0}

        };
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