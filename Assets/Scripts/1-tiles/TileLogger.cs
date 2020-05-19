using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component shows which tile the player is standing on. It is used for debugging.
 */
public class TileLogger : MonoBehaviour {
    [Header("Input")]
    [SerializeField] Tilemap tilemap;

    [Header("Output")]
    [SerializeField] Vector3Int cellPosition;
    [SerializeField] TileBase tile;
    [SerializeField] string tileName;

    void Update()  {
        cellPosition = tilemap.WorldToCell(transform.position);
        tile = tilemap.GetTile(cellPosition);
        tileName = tile.name;
    }

}
