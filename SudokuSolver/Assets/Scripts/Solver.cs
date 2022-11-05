using UnityEngine;

namespace SudokuSolver
{
    /// <summary>
    /// This class serves as the communication between the logic and Unity. It has the list of solving algorithms that will be used, and some logic to solve the sudoku
    /// either step by step or in one go. If the latter option is chosen, it will simply take the step by step approach until there is nothing to be done, because
    /// the sudoku has been solved or because there is no solution available.
    /// TODO may divide into two: one which has the actual solver, and a more separated bridge.
    /// </summary>
    public class Solver : MonoBehaviour
    {
        [SerializeField] private SolvingAlgorithm[] _solvingAlgorithms;

        private Sudoku _sudoku;
        private bool _finished;

        private void Start()
        {
            _finished = false;
            _sudoku = new Sudoku();
            
            //TODO remove
            _solvingAlgorithms = new[] { new NakedSingle() };
        }

        /// <summary>
        /// Uses different algorithms to try to find a number in the sudoku.
        /// </summary>
        /// <returns>True if a number was found, false if no number could be found.</returns>
        public bool TakeStep()
        {
            for (int i = 0; i < _solvingAlgorithms.Length; i++)
            {
                ValueFound valueFound = _solvingAlgorithms[i].FindNumber(_sudoku);
                if (valueFound._valid)
                {
                    //TODO update GUI
                    _sudoku.FillCell(valueFound._point, valueFound._number);
                    Debug.Log("Value found: " + valueFound._number + " at row " + valueFound._point._row + ", column "+ valueFound._point._column);
                    return true;
                }
            }

            return false;
        }

        public void Solve()
        {
            while (!_finished)
            {
                _sudoku.RemovePencilMarks();
                _finished = !TakeStep();
            }
            
            // TODO 
            // Check that it was correctly solved, or if no solution can be found (because there is ambiguity or the algorithms didnt work. 
        }
        
    }
}