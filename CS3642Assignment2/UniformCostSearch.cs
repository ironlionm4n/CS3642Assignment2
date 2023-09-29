using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3642Assignment2
{
    internal class UniformCostSearch
    {
        // Keeps track of how many nodes has been visited during UCS
        public int numberOfNodesVisited;

        public float elapsedTime;

        // Dictionary to assign values to each move resulting in variable cost for each node
        private readonly Dictionary<string, int> costForMoves = new Dictionary<string, int>()
        {
            { "Up", 1},{ "Down", 1}, { "Right", 1}, { "Left", 1}
        };

        // List of all possible moves with their given Direction to obtain cost
        List<Tuple<int, int, string>> possibleMoves = new List<Tuple<int, int, string>>()
        {
                new Tuple<int, int, string>(-1, 0, "Up"),
                new Tuple<int, int, string>(1, 0, "Down"),
                new Tuple<int, int, string>(0, -1, "Left"),
                new Tuple<int, int, string>(0, 1, "Right")
        };

        public Node UCSAlgorithm(Board board)
        {
            // Priority Queue of Nodes that have been visited
            PriorityQueue<Node, int> boardsQueue = new PriorityQueue<Node, int>();
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // HashSet to hold each visited state so no state can be visited more than once
            HashSet<string> visitedStates = new HashSet<string>();

            // Root node is the starting board state
            Node rootNode = new Node(board.GetBoardState);

            // Add the root node to the queue
            boardsQueue.Enqueue(rootNode, rootNode.Cost);

            // While there are more boards in the Queue check if it matches the goal state and return if it does
            // If the board does not match the goal state then generate successor states that need to be visited and add them to the queue
            while (boardsQueue.Count > 0)
            {
                // Increment the number nodes visited by 1
                numberOfNodesVisited++;

                // Dequeue the first node put in the queue
                Node currentNode = boardsQueue.Dequeue();
                //Console.WriteLine($"Cost: {currentNode.Cost}");
                // Check if the current node's board matches the goal state
                if (CheckGoalState(currentNode.BoardState))
                {

                    // For visual purposes I logged these out in the console
                    //Console.WriteLine("Found Solution State!");
                    //PrintSuccessorState(currentNode.BoardState);

                    stopwatch.Stop();
                    elapsedTime = stopwatch.ElapsedMilliseconds;

                    // Return the current node that matches the goal state
                    return currentNode;
                }

                // If we have reached this point we have not found a goal state and need to generate the successor states
                foreach (var successor in GetSuccessorStates(currentNode))
                {
                    // Add the string representation of the board state to the hashset so I dont visit the same board state more than once
                    var boardStateHash = string.Join(",", successor.BoardState.Cast<int>());

                    // If the hashset does not contain the current board state then add it to the hashset
                    if (!visitedStates.Contains(boardStateHash))
                    {
                        visitedStates.Add(boardStateHash);

                        // Enqueue each successor board state into the queue of nodes to check for goal state
                        boardsQueue.Enqueue(successor, successor.Cost);
                    }
                }
            }

            stopwatch.Stop();
            elapsedTime = stopwatch.ElapsedMilliseconds;

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

            // Loop through each move in the moves list to update the position of the blank space and swap the value that was in the position it now is in
            foreach (var move in possibleMoves)
            {

                // New x & y coordinate for the blank space
                var newXCoordinate = xCoordinate + move.Item1;
                var newYCoordinate = yCoordinate + move.Item2;

                // If the new coordinates are within the boundaries of the array then generate a new successor board state
                if (newXCoordinate >= 0 && newXCoordinate < 3 && newYCoordinate >= 0 && newYCoordinate < 3)
                {

                    // New instance of a successive board state
                    int[,] successiveBoard = (int[,])node.BoardState.Clone();

                    // Swap the blank space with the value that is where it is going to move to
                    successiveBoard[xCoordinate, yCoordinate] = successiveBoard[newXCoordinate, newYCoordinate];
                    successiveBoard[newXCoordinate, newYCoordinate] = -1;
        
                    // add the total cost for this successive board based on direction of move
                    var totalCost = node.Cost + costForMoves[move.Item3];

                    // Add the new successor board state to the list of successor board states
                    successorStates.Add(new Node(successiveBoard, totalCost));
                }
            }

            // Return the list of successor board states
            return successorStates;
        }

        // Function to print out the successor state so I can confirm it matches the goal state
        public void PrintSuccessorState(int[,] successorBoard)
        {
            Console.WriteLine("Successor Board State:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (successorBoard[i, j] == -1)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(successorBoard[i, j]);
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
