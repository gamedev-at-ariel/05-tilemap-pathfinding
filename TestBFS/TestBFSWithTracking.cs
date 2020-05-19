using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using IntPair = System.ValueTuple<int, int>;


namespace TestBFSWithTracking {

    class BFSInt : BFSWithTracking<int> {
        public override IEnumerable<int> Neighbors(int node) {
            //Console.WriteLine("Neighbors " + node);
            yield return node + 1;
            yield return node - 1;
        }

        public override IEnumerator OnOpen(int node) {
            //Console.WriteLine("Open " + node);
            return null;
        }

        public override IEnumerator OnEndFound() {
            //Console.WriteLine("End found");
            return null;
        }

        public override IEnumerator OnEndNotFound() {
            //Console.WriteLine("End not found");
            return null;
        }

        public override IEnumerator OnBackTrack(int node) {
            //Console.WriteLine("Back track " + node);
            return null;
        }

        public override IEnumerator OnFocus(int node) {
            //Console.WriteLine("Focus " + node);
            return null;
        }

        public override IEnumerator OnClose(int node) {
            //Console.WriteLine("Close " + node);
            return null;
        }

    }



    class BFSPair : BFSWithTracking<IntPair> {
        public override IEnumerable<IntPair> Neighbors(IntPair node) {
            yield return (node.Item1, node.Item2 + 1);
            yield return (node.Item1, node.Item2 - 1);
            yield return (node.Item1 + 1, node.Item2);
            yield return (node.Item1 - 1, node.Item2);
        }

        public override IEnumerator OnEndFound() {
            //Console.WriteLine("End found");
            return null;
        }

        public override IEnumerator OnEndNotFound() {
            //Console.WriteLine("End not found");
            return null;
        }

        public override IEnumerator OnBackTrack(IntPair node) {
            //Console.WriteLine("Back track " + node);
            return null;
        }

        public override IEnumerator OnFocus(IntPair node) {
            //Console.WriteLine("Focus " + node);
            return null;
        }

        public override IEnumerator OnOpen(IntPair node) {
            //Console.WriteLine("Open " + node);
            return null;
        }

        public override IEnumerator OnClose(IntPair node) {
            //Console.WriteLine("Close " + node);
            return null;
        }

    }


    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Start BFS Test");

            var bfs = new BFSInt();
            var path = bfs.GetPath(90, 95);
            var pathString = String.Join(",", path.ToArray());
            Console.WriteLine("path is: " + pathString);
            Debug.Assert(pathString == "90,91,92,93,94,95");

            path = bfs.GetPath(85, 80);
            pathString = String.Join(",", path.ToArray());
            Console.WriteLine("path is: " + pathString);
            Debug.Assert(pathString == "85,84,83,82,81,80");

            var bfs2 = new BFSPair();
            var path2 = bfs2.GetPath((9, 5), (7, 6));
            pathString = String.Join(",", path2.ToArray());
            Console.WriteLine("path is: " + pathString);
            Debug.Assert(pathString == "(9, 5),(8, 5),(7, 5),(7, 6)");

            // Here we should get an empty path because of maxiterations:
            int maxiterations = 1000;
            path2 = bfs2.GetPath((9, 5), (-7, -6), maxiterations);
            pathString = String.Join(",", path2.ToArray());
            Console.WriteLine("path is: " + pathString);
            Debug.Assert(pathString == "");

            Console.WriteLine("End BFS Test");
        }
    }

}
