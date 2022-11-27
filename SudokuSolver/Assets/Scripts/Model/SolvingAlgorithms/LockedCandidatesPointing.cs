using System;
using System.Collections.Generic;
using UnityEngine;

namespace SudokuSolver.Model
{
    /// <summary>
    /// A locked candidate of type 1, also known as pointing pairs or triplets, can be found when a number in a region
    /// can only appear in a certain row or column. In that case, the number cannot appear in that row or column outside of the region.
    /// This algorithm does not place numbers in the grid, but it removes pencil marks, which can result in placing numbers in the next step.
    /// </summary>
    public class LockedCandidatesPointing : SolvingAlgorithm
    {        
        public List<ValueFound> FindNumber(Sudoku sudoku)
        {
            for (int region = 0; region < sudoku.GetSize(); region++)
            {
                // Goes through each number 1-9 to see where each one could be placed
                for (int mark = 1; mark <= sudoku.GetSize(); mark++)
                {
                    List<Point> places = sudoku.GetPositionsForNumberInRegion(mark, region);

                    // This should never happen, but I'll add this in case there is human interaction in the future.
                    if (places.Count < 1)
                        throw new Exception("Number " + mark + 1 + " cannot appear in region " + region + ". Something went wrong");
                    
                    // If there are more than three places where the number could be in the region, they cannot be aligned.
                    if(places.Count == 1 || places.Count > 3) continue;
                    
                    // The rest of the points will be compared to this one;
                    // If they are all in the same column or row, they are "locked candidates"
                    Point a = places[0];
                    bool alignedInRow = true, alignedInColumn = true;

                    for (int point = 1; point < places.Count; point++)
                    {
                        alignedInColumn = (alignedInColumn && places[point]._column == a._column);
                        alignedInRow = (alignedInRow && places[point]._row == a._row);
                        
                        // If they are not aligned in any way, there is no chance they will be. There is no need to continue.
                        if(!alignedInColumn && !alignedInRow) break;
                    }
                    
                    Point movement = new Point(0, 0);
                    if (alignedInColumn)
                    {
                        movement._row = 1;
                        a._row = 0;
                    }
                    else if (alignedInRow)
                    {
                        movement._column = 1;
                        a._column = 0;
                    }
                    else continue;
                    
                    for (int k = 0; k < sudoku.GetSize(); k++)
                    {
                        //If the point is in a different region, its mark is removed
                        if (sudoku.GetRegion(a) != region)
                        {
                            sudoku.RemoveMark(a, mark);
                            Debug.LogWarning("Value removed: " + mark + 
                                             "             at row " + a._row +
                                             ", column " + a._column);
                        }
                        
                        a += movement;
                    }
                }
            }

            // This algorithm does not place any number in the sudoku; it shall return an empty list.
            return new List<ValueFound>();
        }
    }
}