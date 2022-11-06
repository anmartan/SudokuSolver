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
        private readonly SudokuCell[,] _grid;
        private readonly Dictionary<int, List<Point>> _regions;
        private const int _SIZE_ = 9;
        private readonly List<Restriction> _restrictions;
        
        public Sudoku()
        {
            _grid = new SudokuCell[_SIZE_, _SIZE_];
            for (int i = 0; i < _SIZE_; i++)
            {
                for (int j = 0; j < _SIZE_; j++)
                    _grid[i, j] = new SudokuCell();
            }
            _regions = new Dictionary<int, List<Point>>(_SIZE_);
            
            // TODO this will be read but for now its hardcoded
            _restrictions = new List<Restriction>
                {   new RowRestriction(), 
                    new ColumnRestriction(), 
                    new RegionRestriction() };
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
                for (int j = 0; j < _SIZE_; j++)
                {
                    int region = GetRegion(new Point(i, j));
                    _regions[region].Add(new Point(i, j));
                    file.Write(region + " ");
                }

                file.WriteLine();
            }
            file.Close();
            
            for (int i = 0; i < _SIZE_; i++)
            {
                for (int j = 0; j < _SIZE_; j++)
                {
                    int value = nakedSingles[i, j];
                    _grid[i,j].Init(value);
                    if(value != 0) 
                        RemovePencilMarks(new Point(i, j), value);
                }
            }
        }

        // TODO Auxiliary, remove 
        private int GetRegion(Point point)
        {
            return ((point._column / 3)) + ((point._row/3) * 3);
        }
        
        public List<Point> GetCellsInRegion(int region)
        {
            return _regions[region];
        }
        
        public List<Point> GetCellsInRegion(Point point)
        {
            return _regions[GetRegion(point)];
        }

        public int GetSize() { return _SIZE_; }

        public List<int> GetPossibleValuesInCell(Point point)
        {
            return _grid[point._row, point._column].GetPossibleValues();
        }

        public bool IsCellFilled(Point point)
        {
            return _grid[point._row, point._column].GetCurrentValue() != 0;
        }

        public void FillCell(Point point, int number)
        {
            _grid[point._row, point._column].SetValue(number);
            _regions[GetRegion(point)].Add(point);
        }
        
        public void RemovePencilMarks(Point point, int mark)
        {
            foreach (Restriction restriction in _restrictions)
            {
                restriction.RemoveMarks(this, point, mark);
            }
        }

        public void RemoveMark(Point point, int mark)
        {
            _grid[point._row, point._column].RemovePencilMark(mark);
        }

        private static readonly int[,] nakedSingles = new int[,]
        {
            {2,0,3,0,0,0,0,5,6},
            {0,6,1,0,2,0,0,0,0},
            {0,0,0,4,0,0,1,0,0},
            {3,0,0,0,6,0,0,0,0},
            {0,9,0,2,0,0,0,4,1},
            {0,0,0,9,5,0,6,8,0},
            {5,0,0,0,4,8,0,0,0},
            {0,8,6,0,9,2,5,7,4},
            {4,3,9,0,7,1,2,6,0},

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