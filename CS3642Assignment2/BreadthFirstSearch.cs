using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3642Assignment2
{
    /// <summary>
    /// This class represents the implementation for Breadth-First search for solving the 8-puzzle problem
    /// </summary>
    class BreadthFirstSearch
    {
        // Counter that represents how many nodes were visited during the search
        public int numberOfNodesVisited = 0;

        // This is the actual function for solving the 8-puzzle problem using Breadth-First search
        public Node BFSAlgorithm(Board board)
        {
            // Queue of Nodes that have been visited
            Queue<Node> boards = new Queue<Node>();

            // HashSet to hold each visited state so no state can be visited more than once
            HashSet<string> visitedStates = new HashSet<string>();

            // Root node is the starting board state
            Node rootNode = new Node(board.GetBoardState);

            // Add the root node to the queue
            boards.Enqueue(rootNode);

            // While there are more boards in the Queue check if it matches the goal state and return if it does
            // If the board does not match the goal state then generate successor states that need to be visited and add them to the queue
            while(boards.Count > 0)
            {
                // Increment the number nodes visited by 1
                numberOfNodesVisited++;

                // Dequeue the first node put in the queue
                Node currentNode = boards.Dequeue();

                // Check if the current node's board matches the goal state
                if (CheckGoalState(currentNode.BoardState)) {

                    // For visual purposes I logged these out in the console
                    Console.WriteLine("Found Solution State!");
                    PrintSuccessorState(currentNode.BoardState);

                    // Return the current node that matches the goal state
                    return currentNode;
                }

                // If we have reached this point we have not found a goal state and need to generate the successor states
                foreach(var successor in GetSuccessorStates(currentNode))
                {
                    // Add the string representation of the board state to the hashset so I dont visit the same board state more than once
                    var boardStateHash = string.Join(",", successor.BoardState.Cast<int>());

                    // If the hashset does not contain the current board state then add it to the hashset
                    if(!visitedStates.Contains(boardStateHash))
                    {
                        visitedStates.Add(boardStateHash);

                        // Enqueue each successor board state into the queue of nodes to check for goal state
                        boards.Enqueue(successor);
                    }
                }
            }

            // Returning null as no goal state was found in the breadth-first search
            return null;
        }

        // Checks each position in the board if it matches the goal state, return false as soon as a mismatch is found
        public bool CheckGoalState(int[,] currentState)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // If the current board state doesn't match the goal state then return false 
                    if (currentState[i, j] != Board.GoalState[i, j]) return false;
                }
            }

            // We have a current state that matches the goal state!
            return true;
        }

        // Returns a list with all of the successor states
        public List<Node> GetSuccessorStates(Node node)
        {
            var successorStates = new List<Node>();

            // The x & y coordinate for the blank space
            int xCoordinate = 0, yCoordinate = 0;

            // Boolean to terminate the outer loop once the blank space has been found
            bool notFound = true;

            // Loop through each row until we have reached the end and we have not found the blank space
            for (int i = 0; i < 3 && notFound; i++)
            {
                // Loop through each column
                for (int j = 0; j < 3; j++)
                {
                    // If the position in this node's board state is -1 it is the blank space
                    if (node.BoardState[i, j] == -1)
                    {
                        // Found the blank space
                        notFound = false;

                        // Update the x and y coordinates to represent this position in the board state
                        xCoordinate = i;
                        yCoordinate = j;

                        // break out of the inner loop since we found the blank space
                        break;
                    }
                }
            }

            // These moves represent how the blank space can be moved to adjacent tiles
            int[] moveUp = new int[] { -1, 0 };
            int[] moveDown = new int[] { 1, 0 };
            int[] moveLeft = new int[] { 0, -1 };
            int[] moveRight = new int[] { 0, 1 };

            // Adding all the moves to a list to generate new board states with a swapped blank space
            List<int[]> moves = new List<int[]> { moveUp, moveDown, moveLeft, moveRight };

            // Loop through each move in the moves list to update the position of the blank space and swap the value that was in the position it now is in
            foreach (var move in moves)
            {

                // New x & y coordinate for the blank space
                var newXCoordinate = xCoordinate + move[0];
                var newYCoordinate = yCoordinate + move[1];

                // If the new coordinates are within the boundaries of the array then generate a new successor board state
                if (newXCoordinate >= 0 && newXCoordinate < 3 && newYCoordinate >= 0 && newYCoordinate < 3)
                {
                    // New instance of a successive board state
                    int[,] successiveBoard = (int[,]) node.BoardState.Clone();

                    // Swap the blank space with the value that is where it is going to move to
                    successiveBoard[xCoordinate, yCoordinate] = successiveBoard[newXCoordinate, newYCoordinate];
                    successiveBoard[newXCoordinate, newYCoordinate] = -1;

                    // Add the new successor board state to the list of successor board states
                    successorStates.Add(new Node(successiveBoard));
                }
            }

            // Return the list of successor board states
            return successorStates;
        }

        // Function to print out the successor state so I can confirm it matches the goal state
        public void PrintSuccessorState(int[,] successorBoard)
        {
            Console.WriteLine("Successor Board State:");
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (successorBoard[i,j] == -1)
                    {
                        Console.Write("X");
                    } else
                    {
                        Console.Write(successorBoard[i,j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        // Reset the number of nodes visited to 0 after each run of the BFS algorithm
        public void ResetNumberOfVisitedNodes()
        {
            numberOfNodesVisited = 0;
        }
    }
}
