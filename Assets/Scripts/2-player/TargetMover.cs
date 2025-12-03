using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component moves its object towards a given target position.
 */
public class TargetMover: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f;

    [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
    [SerializeField] int maxIterations = 1000;

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld;

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid;

    // The current shortest path to the target, in grid coordinates.
    private List<Vector3Int> currentPathInGrid = null;

    protected bool atTarget = true;  // This property is set to "true" whenever the object has already found the target.

    public void SetTarget(Vector3 newTargetInWorld) {
        if (targetInWorld != newTargetInWorld) {
            targetInWorld = newTargetInWorld;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
            currentPathInGrid = null;
        }
    }

    public Vector3 GetTarget() {
        return targetInWorld;
    }

    private TilemapGraph tilemapGraph = null;
    private float timeBetweenSteps;

    protected virtual void Start() {
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get());
        timeBetweenSteps = 1 / speed;
        MoveTowardsTheTarget();
    }

    async void MoveTowardsTheTarget() {
        for(;;) {
            await Awaitable.WaitForSecondsAsync(timeBetweenSteps);
            if (enabled && !atTarget)
                MakeOneStepTowardsTheTarget();
        }
    }

    private void MakeOneStepTowardsTheTarget()
    {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        if (currentPathInGrid == null || currentPathInGrid[0] != startNode)
        {  // calculate new shortest path
            Vector3Int endNode = targetInGrid;
            currentPathInGrid = BFS.GetPath(tilemapGraph, startNode, endNode, maxIterations);
            Debug.Log("new shortestPath = " + string.Join(" , ", currentPathInGrid));
            if (currentPathInGrid.Count == 0) {
                Debug.LogWarning($"No path found between {startNode} and {endNode}");
            }
        }
        if (currentPathInGrid.Count <= 1) {
            Debug.Log($"Found target");
            atTarget = true;
        } else { // currentPathInGrid contains both source and target.
            currentPathInGrid.RemoveAt(0);  // this was the current node
            Vector3Int nextNode = currentPathInGrid[0];  // this is the new node
            transform.position = tilemap.GetCellCenterWorld(nextNode);
        }
    }
}
