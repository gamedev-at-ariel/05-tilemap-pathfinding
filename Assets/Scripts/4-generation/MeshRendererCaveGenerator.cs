using System.Collections;
using UnityEngine;

/**
 * This class demonstrates the CaveGenerator on a black-and-white MeshRenderer.
 * 
 * Based on Unity tutorial https://www.youtube.com/watch?v=v7yyZZjF1z4 
 * Code by Habrador: https://github.com/Habrador/Unity-Programming-Patterns/blob/master/Assets/Patterns/7.%20Double%20Buffer/Cave/GameController.cs
 * 
 * Adapted by: Erel Segal-Halevi
 * Since: 2020-12
 */

public class MeshRendererCaveGenerator: MonoBehaviour {
    public MeshRenderer displayPlaneRenderer;

    [Tooltip("The percent of walls in the initial random map")]
    [Range(0, 1)]
    [SerializeField] float randomFillPercent = 0.5f;

    [Tooltip("Length and height of the grid")]
    [SerializeField] int gridSize = 100;

    [Tooltip("How many steps do we want to simulate?")]
    [SerializeField] int simulationSteps = 20;

    [Tooltip("For how long will we pause between each simulation step so we can look at the result?")]
    [SerializeField] float pauseTime = 1f;

    private CaveGenerator caveGenerator;

    void Start() {
        //To get the same random numbers each time we run the script
        Random.InitState(100);

        caveGenerator = new CaveGenerator(randomFillPercent, gridSize);
        caveGenerator.RandomizeMap();

        //For testing that init is working
        GenerateAndDisplayTexture(caveGenerator.GetMap());

        //Start the simulation
        StartCoroutine(SimulateCavePattern());
    }


    //Do the simulation in a coroutine so we can pause and see what's going on
    private IEnumerator SimulateCavePattern() {
        for (int i = 0; i < simulationSteps; i++) {
            yield return new WaitForSeconds(pauseTime);

            //Calculate the new values
            caveGenerator.SmoothMap();

            //Generate texture and display it on the plane
            GenerateAndDisplayTexture(caveGenerator.GetMap());
        }
        Debug.Log("Simulation completed!");
    }



    //Generate a black or white texture depending on if the pixel is cave or wall
    //Display the texture on a plane
    private void GenerateAndDisplayTexture(int[,] data) {
        //We are constantly creating new textures, so we have to delete old textures or the memory will keep increasing
        //The garbage collector is not collecting unused textures
        Resources.UnloadUnusedAssets();
        //We could also use 
        //Destroy(displayPlaneRenderer.sharedMaterial.mainTexture);
        //Or reuse the same texture


        //These two arrays are always the same so we could init them once at start
        Texture2D texture = new Texture2D(gridSize, gridSize);

        texture.filterMode = FilterMode.Point;

        Color[] textureColors = new Color[gridSize * gridSize];

        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                //From 2d array to 1d array
                textureColors[y * gridSize + x] = data[x, y] == 1 ? Color.black : Color.white;
            }
        }

        texture.SetPixels(textureColors);
        texture.Apply();
        displayPlaneRenderer.sharedMaterial.mainTexture = texture;
    }
}
