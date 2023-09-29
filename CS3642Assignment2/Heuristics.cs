using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3642Assignment2
{
    internal class Heuristics
    {
        public static int CalculateTotalManhattanDistance(int[,] board)
        {
            var totalManhattanDistance = 0;

            int[,] goalPositions = new int[8, 2]
            {
                { 0,0 },
                { 0,1 },
                { 0,2 },
                { 0,3 },
                { 1,2 },
                { 2,2 },
                { 2,1 },
                { 1,0 }
            };

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    var tileValue = board[i,j];

                    if(tileValue != -1)
                    {
                        var goalRow = goalPositions[tileValue - 1, 0];
                        var goalColumn = goalPositions[tileValue - 1, 1];
                        totalManhattanDistance += Math.Abs(i - goalRow) + Math.Abs(j - goalColumn);
                    }
                }
            }

            return totalManhattanDistance;
        }
    }
}
