using System;

Main();

void print(int[,] data, int gridSize) {
    for (int x=0; x < gridSize; ++x) {
        for (int y = 0; y<gridSize; ++y) {
            Console.Write(data[x,y]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

void Main() {
    Console.WriteLine("Start CaveGenerator Test");

    int gridSize = 20;
    CaveGenerator caveGenerator = new CaveGenerator(0.5f, gridSize);
    caveGenerator.RandomizeMap();
    int maxSteps = 5;
    for (int i=0; i<maxSteps; ++i) {
        Console.WriteLine($"Step {i} / {maxSteps}: ");
        print(caveGenerator.GetMap(),gridSize);
        // Console.ReadKey();
        System.Threading.Thread.Sleep(1000); // wait one second
        caveGenerator.SmoothMap();
    }

    Console.WriteLine("End CaveGenerator Test");
}


