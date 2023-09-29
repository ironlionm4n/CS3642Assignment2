using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3642Assignment2
{
    class PerformFunctions
    {
        Board gameBoard = new Board();
        BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch();
        UniformCostSearch uniformCostSearch = new UniformCostSearch();
        BestFirstSearch bestFirstSearch = new BestFirstSearch();
        int counter = 20;
        List<float> runTimes = new List<float>();

        public void PerformBreadthFirstSearch()
        {
            runTimes.Clear();
            var numberOfNodesVisited = 0;
            do
            {
                gameBoard.InitializeBoardState();
                breadthFirstSearch.BFSAlgorithm(gameBoard);
                runTimes.Add(breadthFirstSearch.elapsedTime);
                numberOfNodesVisited += breadthFirstSearch.numberOfNodesVisited;
                breadthFirstSearch.ResetNumberOfVisitedNodes();
                breadthFirstSearch.elapsedTime = 0;
                counter--;
            } while (counter > 0);
            ResetCounter();
            double averageTimeBFS = runTimes.Average();
            using (StreamWriter writer = new StreamWriter("output.txt", true))
            {
                writer.WriteLine($"{ numberOfNodesVisited / counter},{ averageTimeBFS}");
            }
        }

        public void PerformUniformCostSearch()
        {
            runTimes.Clear();
            var numberOfNodesVisited = 0;
            do
            {
                gameBoard.InitializeBoardState();
                uniformCostSearch.UCSAlgorithm(gameBoard);
                runTimes.Add(uniformCostSearch.elapsedTime);
                numberOfNodesVisited += uniformCostSearch.numberOfNodesVisited;
                uniformCostSearch.ResetNumberOfVisitedNodes();
                uniformCostSearch.elapsedTime = 0;
                counter--;
            } while (counter > 0);
            ResetCounter();
            double averageTimeUCS = runTimes.Average();
            using (StreamWriter writer = new StreamWriter("output.txt", true))
            {
                writer.WriteLine($"{numberOfNodesVisited / counter},{averageTimeUCS}");
            }
        }

        public void PerformBestFirstSearch()
        {
            runTimes.Clear();
            var numberOfNodesVisited = 0;
            do
            {
                gameBoard.InitializeBoardState();
                bestFirstSearch.BestFirstSearchAlgorithm(gameBoard);
                runTimes.Add(bestFirstSearch.elapsedTime);
                numberOfNodesVisited += bestFirstSearch.numberOfNodesVisited;
                bestFirstSearch.ResetNumberOfVisitedNodes();
                bestFirstSearch.elapsedTime = 0;
                counter--;
            } while (counter > 0);
            ResetCounter();
            double averageTimeUCS = runTimes.Average();
            using (StreamWriter writer = new StreamWriter("output.txt", true))
            {
                writer.WriteLine($"{numberOfNodesVisited / counter},{averageTimeUCS}");
            }
        }

        private void ResetCounter()
        {
            counter = 20;
        }

    }
}
