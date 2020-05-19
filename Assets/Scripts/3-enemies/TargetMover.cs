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

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld;
    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid;

    public void SetTarget(Vector3 newTarget) {
        targetInWorld = newTarget;
        targetInGrid  = tilemap.WorldToCell(targetInWorld);
    }

    [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
    [SerializeField] int maxIterations = 1000;

    private TilemapBFS tilemapBFS = null;
    private float timeBetweenSteps;

    private void Start() {
        tilemapBFS = new TilemapBFS(tilemap, allowedTiles.Get());
        timeBetweenSteps = 1 / speed;
        StartCoroutine(MoveTowardsTheTarget());
    }

    IEnumerator MoveTowardsTheTarget() {
        for(;;) {
            yield return new WaitForSeconds(timeBetweenSteps);
            if (enabled)
                MakeOneStepTowardsTheTarget();
        }
    }

    private void MakeOneStepTowardsTheTarget() {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        Vector3Int endNode = targetInGrid;
        List<Vector3Int> shortestPath = tilemapBFS.GetPath(startNode, endNode, maxIterations);
        Debug.Log("shortestPath = " + string.Join(" , ",shortestPath));
        if (shortestPath.Count >= 2) {
            Vector3Int nextNode = shortestPath[1];
            transform.position = tilemap.GetCellCenterWorld(nextNode);
        }
    }
}
