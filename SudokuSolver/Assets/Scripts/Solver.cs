using System.Collections.Generic;
using UnityEngine;

namespace SudokuSolver
{
    public class Solver : MonoBehaviour
    {
        [SerializeField] private readonly SolvingAlgorithm[] _solvingAlgorithms;

        private Sudoku _sudoku;
        private bool _finished;

        private void Start()
        {
            _finished = false;
            _sudoku = new Sudoku();
        }

        /// <summary>
        /// Uses different algorithms to try to find a number in the sudoku.
        /// </summary>
        /// <returns>True if a number was found, false if no number could be found.</returns>
        public bool TakeStep()
        {
            for (int i = 0; i < _solvingAlgorithms.Length; i++)
            {
                if (_solvingAlgorithms[i].FindNumber(_sudoku))
                {
                    //TODO update GUI
                    return true;
                }
            }

            return false;
        }

        public void Solve()
        {
            while (!_finished)
            {
                _finished = !TakeStep();
            }
            
            // TODO 
            // Check that it was correctly solved, or if no solution can be found (because there is ambiguity or the algorithms didnt work. 
        }
        
    }
}