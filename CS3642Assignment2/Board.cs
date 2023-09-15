using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CS3642Assignment2
{
    class Board
    {
        int[,] boardState = new int[3, 3];
        public int[,] GetBoardState => boardState;

        public static int[,] GoalState = new int[3, 3] { { 1, 2, 3 }, { 8, -1, 4 }, { 7, 6, 5 } };

        List<int> boardValues = new List<int>() { -1, 1, 2, 3, 4, 5, 6, 7, 8 };
        Random random = new Random();

        public void InitializeBoardState()
        {
            var tempListValues = new List<int>(boardValues);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var listIndex = random.Next(0, tempListValues.Count);
                    boardState[i, j] = tempListValues[listIndex];
                    tempListValues.RemoveAt(listIndex);
                }
            }
        }

        public void PrintBoardState()
        {
            Console.WriteLine("Initial Board State:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (boardState[i, j] == -1)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(boardState[i, j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
