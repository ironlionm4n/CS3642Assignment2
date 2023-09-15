using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3642Assignment2
{
    class Node
    {
        public int[,] BoardState { get; set; }
        public Node? Parent { get; set; }
        public Node(int[,] boardState, Node? parentNode = null)
        {
            BoardState = boardState;
            Parent = parentNode;
        }
    }
}
