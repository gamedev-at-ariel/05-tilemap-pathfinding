using System;
using System.Collections.Generic;
using System.Diagnostics;

using IntPair = System.ValueTuple<int, int>;


/**
 * A unit-test for the abstract BFS algorithm.
 * To run it, use the solution in 05-tilemap-pathfinding/TestBFS/TestBFS.sln.
 * 
 * @author Erel Segal-Halevi
 * @since 2020-02
 */
namespace TestBFS {

    class IntGraph: IGraph<int>
    {  // IGraph is defined in the file ./IGraph.cs (a copy is found in Assets/Scripts/0-bfs/IGraph.cs)
        public IEnumerable<int> Neighbors(int node) {
            yield return node + 1;
            yield return node - 1;
        }
    }

    class IntPairGraph : IGraph<IntPair> {
        public IEnumerable<IntPair> Neighbors(IntPair node) {
            yield return (node.Item1, node.Item2 + 1);
            yield return (node.Item1, node.Item2 - 1);
            yield return (node.Item1 + 1, node.Item2);
            yield return (node.Item1 - 1, node.Item2);
            //yield return (node.Item1 - 1, node.Item2+1);
        }
    }

    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Start BFS Test");

            var intGraph = new IntGraph();
            var path = BFS.GetPath(intGraph, 90, 95);
            var pathString = string.Join(",", path.ToArray());
            Console.WriteLine("path is: " + pathString);
            Debug.Assert(pathString == "90,91,92,93,94,95");
            path = BFS.GetPath(intGraph, 85, 80);
            pathString = string.Join(",", path.ToArray());
            Console.WriteLine("path is: " + pathString);
            Debug.Assert(pathString == "85,84,83,82,81,80");

            var intPairGraph = new IntPairGraph();
            var path2 = BFS.GetPath(intPairGraph, (9, 5), (7, 6));
            pathString = string.Join(",", path2.ToArray());
            Console.WriteLine("path is: " + pathString);
            Debug.Assert(pathString == "(9, 5),(9, 6),(8, 6),(7, 6)");
            Debug.Assert(path2.Count ==4);

            // Here we should get an empty path because of maxiterations:
            int maxiterations = 1000;
            path2 = BFS.GetPath(intPairGraph, (9, 5), (-7,-6), maxiterations); 
            pathString = string.Join(",", path2.ToArray());
            Console.WriteLine("path is: " + pathString);
            Debug.Assert(pathString == "");

            Console.WriteLine("End BFS Test");
        }
    }

}
