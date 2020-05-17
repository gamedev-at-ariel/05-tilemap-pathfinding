using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    private Vector3 NewPosition() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            return transform.position + Vector3.left;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            return transform.position + Vector3.right;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            return transform.position + Vector3.down;
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            return transform.position + Vector3.up;
        } else {
            return transform.position;
        }
    }

    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()  {
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);
        if (allowedTiles.Contain(tileOnNewPosition)) {
            transform.position = newPosition;
        } else {
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }
    }
}
