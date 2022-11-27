using System.Collections.Generic;
using UnityEngine;

namespace SudokuSolver.Model
{
    /// <summary>
    /// This class serves as the communication between the logic and Unity. It has the list of solving algorithms that will be used, and some logic to solve the sudoku
    /// either step by step or in one go. If the latter option is chosen, it will simply take the step by step approach until there is nothing to be done, because
    /// the sudoku has been solved or because there is no solution available.
    /// TODO may divide into two: one which has the actual solver, and a separated bridge between logic and view.
    /// </summary>
    public class Solver
    {
        // TODO this should be read from somewhere
        private SolvingAlgorithm[] _solvingAlgorithms;

        private Sudoku _sudoku;
        private bool _finished;

        public Solver()
        {
            _finished = false;
            _sudoku = new Sudoku();
            
            //TODO remove
            _solvingAlgorithms = new SolvingAlgorithm[3];
            _solvingAlgorithms[0] = new NakedSingle();
            _solvingAlgorithms[1] = new HiddenSingle();
            _solvingAlgorithms[2] = new LockedCandidatesPointing();
        }

        /// <summary>
        /// Uses different algorithms to try to find a number in the sudoku.
        /// </summary>
        /// <returns>List of values found; it stops searching as soon as an algorithm returns some values.</returns>
        public List<ValueFound> TakeStep()
        {
            List<ValueFound> valuesFound = new List<ValueFound>();
            for (int i = 0; i < _solvingAlgorithms.Length; i++)
            {
                valuesFound = _solvingAlgorithms[i].FindNumber(_sudoku);
                foreach (var valueFound in valuesFound)
                {
                    //TODO update GUI
                    _sudoku.FillCell(valueFound._point, valueFound._number);
                    _sudoku.RemoveAllImpossiblePencilMarks(valueFound._point, valueFound._number);
                }

                if (valuesFound.Count != 0) return valuesFound;
            }

            return valuesFound;
        }
    }
}