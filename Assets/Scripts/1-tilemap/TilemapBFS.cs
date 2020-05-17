using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * Finds a path on a tilemap, using only the allowed tiles.
 */
public class TilemapBFS: BFS<Vector3Int> {

    private Tilemap tilemap;
    private TileBase[] allowedTiles;

    private float delay = 0.2f;
    private bool showProgress = true;

    public TilemapBFS(Tilemap tilemap, TileBase[] allowedTiles, float delay, bool showProgress) {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
        this.delay = delay;
        this.showProgress = showProgress;
    }

    static Vector3Int[] directions = {
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, 1, 0),
        };

    public override IEnumerable<Vector3Int> Neighbors(Vector3Int searchFocus) {
        foreach (var direction in directions) {
            Vector3Int neighborPos = searchFocus + direction;
            TileBase neighborTile = tilemap.GetTile(neighborPos);
            if (allowedTiles.Contains(neighborTile))
                yield return neighborPos;
        }
    }

    public override IEnumerator OnBackTrack(Vector3Int node) {
        if (showProgress) {
            Debug.Log("Backtrack " + node);
            yield return new WaitForSeconds(delay);
        } else {
            yield break;
        }
    }

    public override IEnumerator OnClose(Vector3Int node) {
        if (showProgress)
            Debug.Log("Close " + node);
        return null;
    }

    public override IEnumerator OnEndFound() {
        if (showProgress)
            Debug.Log("Found end position!");
        return null;
    }

    public override IEnumerator OnEndNotFound() {
        Debug.Log("End position not found!");
        return null;
    }

    public override IEnumerator OnFocus(Vector3Int node) {
        if (showProgress)
            Debug.Log("Focus " + node);
        return null;
    }

    public override IEnumerator OnOpen(Vector3Int node) {
        if (showProgress) {
            Debug.Log("Open " + node);
            yield return new WaitForSeconds(delay);
        } else {
            yield break;
        }
    }
}
