using System;

namespace TestCaveGenerator {

    class Program {

        static int gridSize = 30;

        static void print(int[,] data) {
            for (int x=0; x < gridSize; ++x) {
                for (int y = 0; y<gridSize; ++y) {
                    Console.Write(data[x,y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main(string[] args) {
            CaveGenerator caveGenerator = new CaveGenerator(0.5f, gridSize);
            caveGenerator.RandomizeMap();
            for (int i=0; i<10; ++i) {
                print(caveGenerator.GetMap());
                Console.ReadKey();
                caveGenerator.SmoothMap();
            }

            Console.WriteLine("End CaveGenerator Test");
        }
    }
}
