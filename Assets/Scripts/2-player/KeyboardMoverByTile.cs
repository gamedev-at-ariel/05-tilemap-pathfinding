using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
[RequireComponent(typeof(SnapToGrid))]
public class KeyboardMoverByTile: KeyboardMover {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    void Update() {
        if (moveAction.WasPerformedThisFrame()) {
            GetComponent<TargetMover>()?.SetNoTarget();
            Vector3 movement = moveAction.ReadValue<Vector2>(); // Implicitly convert Vector2 to Vector3, setting z=0.
            Vector3 newLogicalPosition = GetComponent<SnapToGrid>().LogicalPosition + movement;
            Vector3Int cellPosition = tilemap.WorldToCell(newLogicalPosition);
            TileBase tileOnNewPosition = tilemap.GetTile(cellPosition);
            if (allowedTiles.Contains(tileOnNewPosition)) {
                GetComponent<SnapToGrid>().LogicalPosition = newLogicalPosition;
            } else {
                Debug.LogWarning("You cannot walk on " + tileOnNewPosition + "!");
            }
        }
    }
}
