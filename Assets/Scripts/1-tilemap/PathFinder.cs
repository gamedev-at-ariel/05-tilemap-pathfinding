using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component moves its object towards the given target.
 */
public class PathFinder: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    [SerializeField] float delay = 0;
    [SerializeField] bool showProgress = true;

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f;

    [Tooltip("The object that we try to find")]
    [SerializeField] Transform target = null;

    private void Start() {
        tilemapBFS = new TilemapBFS(tilemap, allowedTiles.Get(), delay, showProgress);
        timeBetweenSteps = 1 / speed;
        timeBeforeNextStep = timeBetweenSteps;
    }

    void Update()  {
        timeBeforeNextStep -= Time.deltaTime;
        if (timeBeforeNextStep <= 0) {
            MakeOneStepTowardsTheTarget();
            timeBeforeNextStep += timeBetweenSteps;
        }
    }


    private TilemapBFS tilemapBFS = null;
    private float timeBeforeNextStep = 0;
    private float timeBetweenSteps;

    private void MakeOneStepTowardsTheTarget() {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        Vector3Int endNode = tilemap.WorldToCell(target.position);
        var shortestPath = tilemapBFS.GetPath(startNode, endNode);
        Debug.Log("shortestPath = " + string.Join(" , ",shortestPath));
        if (shortestPath.Count > 1) {
            Vector3Int nextNode = shortestPath[1];
            transform.position = tilemap.GetCellCenterWorld(nextNode);
        }
    }
}
