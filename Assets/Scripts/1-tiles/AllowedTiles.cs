using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles.
 * Such a list is used both for pathfinding and for movement.
 */
public class AllowedTiles : MonoBehaviour  {
    [SerializeField] TileBase[] allowedTiles = null;

    public bool Contains(TileBase tile) {
        return allowedTiles.Contains(tile);
    }

    public TileBase[] Get() { return allowedTiles;  }
}
