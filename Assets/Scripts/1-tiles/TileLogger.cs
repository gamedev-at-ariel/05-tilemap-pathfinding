using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component shows which tile the player is standing on. It is used for debugging.
 */
public class TileLogger : MonoBehaviour {
    [Header("Input")]
    [SerializeField] Tilemap tilemap = null;

    [Header("Output")]
    [SerializeField] Vector3Int cellPosition;
    [SerializeField] TileBase tile = null;
    [SerializeField] string tileName = null;

    void Update()  {
        cellPosition = tilemap.WorldToCell(transform.position);
        tile = tilemap.GetTile(cellPosition);
        tileName = tile.name;
    }

}
