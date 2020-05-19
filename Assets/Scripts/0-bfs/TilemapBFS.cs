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

    public TilemapBFS(Tilemap tilemap, TileBase[] allowedTiles) {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
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
}
