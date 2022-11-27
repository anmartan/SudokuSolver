using UnityEngine;

namespace SudokuSolver.Views
{
    public class SudokuView : MonoBehaviour //TODO this should be an IView, not a monobehaviour
    {

        public void SetValue(Point position, int number)
        {
            Debug.LogWarning($"({position._row + 1}, {position._column + 1}): {number}");
        }

        public void RemovePencilMark(Point position, int mark)
        {
            throw new System.NotImplementedException();
        }
    }
}
