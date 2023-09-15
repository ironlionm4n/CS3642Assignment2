using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CS3642Assignment2
{
    /// <summary>
    /// The Board class represents a single nstance of the puzzle state
    /// </summary>
    class Board
    {
        // Multi-dimensional array that will hold the values for each position in the puzzle
        int[,] boardState = new int[3, 3];
        
        // Expression body method to get the board state
        public int[,] GetBoardState => boardState;

        // Multi-Dimensional array that holds the solution state for the 8-puzzle problem
        public static int[,] GoalState = new int[3, 3] { { 1, 2, 3 }, { 8, -1, 4 }, { 7, 6, 5 } };

        // List of integers that holds all the possible values that can be assigned in the 8-puzzle problem, -1 signifies the blank tile
        List<int> boardValues = new List<int>() { -1, 1, 2, 3, 4, 5, 6, 7, 8 };

        // Instance of the Random class to randomly assign values to each position in the puzzle
        Random random = new Random();

        /// <summary>
        /// Initialization function to assign random values for the starting board state
        /// </summary>
        public void InitializeBoardState()
        {
            // Temporary list of values which has the possible values that can be assigned
            var tempListValues = new List<int>(boardValues);


            // Nested for loop to loop through each position in the board state and assign a value from the possible values
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Randomly choose which value to assign to the current position in the board state
                    var listIndex = random.Next(0, tempListValues.Count);

                    // Assign a value to this position in the board state
                    boardState[i, j] = tempListValues[listIndex];

                    // Remove the value from the list of values possible so there are no duplicate assignments
                    tempListValues.RemoveAt(listIndex);
                }
            }
        }

        /// <summary>
        /// Function that just prints the board state for visual purposes in the Console
        /// </summary>
        public void PrintBoardState()
        {
            Console.WriteLine("Initial Board State:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // If this position in the board state is -1 then that is an empty space, I use X to visualize the empty state
                    if (boardState[i, j] == -1)
                    {
                        Console.Write("X");
                    }
                    // Print the current value of the board state.
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
