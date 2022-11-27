using System.Collections.Generic;
using SudokuSolver.Model;
using SudokuSolver.Views;
using UnityEngine;

namespace Controller
{
    public class Controller: MonoBehaviour
    {
        private Solver _solver;

        [SerializeField] private SudokuView _view;

        private void Start()
        {
            _solver = new Solver();
        }

        public void TakeStep()
        {
            List<ValueFound> valuesFound = _solver.TakeStep();

            for (int i = 0; i < valuesFound.Count; i++)
            {
                _view.SetValue(valuesFound[i]._point, valuesFound[i]._number);
            }
        }

    }
}