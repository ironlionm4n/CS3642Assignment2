using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3642Assignment2
{
    class BreadthFirstSearch
    {
        public int numberOfNodesVisited = 0;
        public Node BFSAlgorithm(Board board)
        {
            Queue<Node> boards = new Queue<Node>();
            HashSet<string> visitedStates = new HashSet<string>();

            Node rootNode = new Node(board.GetBoardState);
            boards.Enqueue(rootNode);

            while(boards.Count > 0)
            {
                numberOfNodesVisited++;
                Node currentNode = boards.Dequeue();
                if (CheckGoalState(currentNode.BoardState)) {
                    Console.WriteLine("Found Solution State!");
                    PrintSuccessorState(currentNode.BoardState);
                    return currentNode;
                }

                foreach(var successor in GetSuccessorStates(currentNode))
                {
                    var boardStateHash = string.Join(",", successor.BoardState.Cast<int>());
                    if(!visitedStates.Contains(boardStateHash))
                    {
                        visitedStates.Add(boardStateHash);
                        boards.Enqueue(successor);
                    }
                }
            }

            return null;
        }

        public bool CheckGoalState(int[,] currentState)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (currentState[i, j] != Board.GoalState[i, j]) return false;
                }
            }

            return true;
        }

        public List<Node> GetSuccessorStates(Node node)
        {
            var successorStates = new List<Node>();

            int xCoordinate = 0, yCoordinate = 0;
            bool notFound = true;

            for (int i = 0; i < 3 && notFound; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (node.BoardState[i, j] == -1)
                    {
                        notFound = false;
                        xCoordinate = i;
                        yCoordinate = j;
                        break;
                    }
                }
            }

            int[] moveUp = new int[] { -1, 0 };
            int[] moveDown = new int[] { 1, 0 };
            int[] moveLeft = new int[] { 0, -1 };
            int[] moveRight = new int[] { 0, 1 };

            List<int[]> moves = new List<int[]> { moveUp, moveDown, moveLeft, moveRight };

            foreach (var move in moves)
            {
                var newXCoordinate = xCoordinate + move[0];
                var newYCoordinate = yCoordinate + move[1];

                if (newXCoordinate >= 0 && newXCoordinate < 3 && newYCoordinate >= 0 && newYCoordinate < 3)
                {
                    int[,] successiveBoard = (int[,])node.BoardState.Clone();
                    successiveBoard[xCoordinate, yCoordinate] = successiveBoard[newXCoordinate, newYCoordinate];
                    successiveBoard[newXCoordinate, newYCoordinate] = -1;
                    successorStates.Add(new Node(successiveBoard, node));
                }
            }

            return successorStates;
        }

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

        public void ResetNumberOfVisitedNodes()
        {
            numberOfNodesVisited = 0;
        }
    }
}
