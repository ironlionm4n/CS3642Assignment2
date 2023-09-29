using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3642Assignment2
{

    // The Node class represents each variation of the board state in the search for the solution state
    class Node
    {
        // This will represent the board state for this current node
        public int[,] BoardState { get; set; }

        // Reference to the parent Node, possibly null
        public Node? Parent { get; set; }

        // The cost to reach this node
        public int Cost { get; set; }

        // The heuristic value for this node
        public int HeuristicValue { get; set; }

        // Node Constructor to set the board state and parent node, parent node is null by default
        public Node(int[,] boardState, int cost = 0, Node? parentNode = null)
        {
            BoardState = boardState;
            Parent = parentNode;
            Cost = cost;
        }
    }
}
