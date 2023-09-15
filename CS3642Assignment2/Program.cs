// See https://aka.ms/new-console-template for more information
using CS3642Assignment2;
using System.Diagnostics;

Board gameBoard = new Board();
BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch();
int counter = 15;
List<long> runTimes = new List<long>();

do
{
    gameBoard.InitializeBoardState();
    Stopwatch stopwatch = Stopwatch.StartNew();
    //gameBoard.PrintBoardState();
    stopwatch.Start();
    breadthFirstSearch.BFSAlgorithm(gameBoard);
    stopwatch.Stop();
    long elapsedTime = stopwatch.ElapsedMilliseconds;
    runTimes.Add(elapsedTime);
    Console.WriteLine($"Number of nodes Visited: {breadthFirstSearch.numberOfNodesVisited}");
    breadthFirstSearch.ResetNumberOfVisitedNodes();
    counter--;
} while (counter > 0);

double averageTime = runTimes.Average();
Console.WriteLine($"Average Runtime: {averageTime}");

