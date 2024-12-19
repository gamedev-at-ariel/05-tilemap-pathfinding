using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile: KeyboardMover {
    [SerializeField] Tilemap tilemap = null;
//    [SerializeField] TileBase[] allowedTiles = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()  {
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);
        if (allowedTiles.Contains(tileOnNewPosition)) {
            transform.position = newPosition;
        } else {
            Debug.LogWarning("You cannot walk on " + tileOnNewPosition + "!");
        }
    }
}
