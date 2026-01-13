using UnityEngine;
using UnityEngine.Tilemaps;


// This component makes its object (the player) move only on the tilemap cells.
public class SnapToGrid : MonoBehaviour {
    [SerializeField] Tilemap tilemap;
    private Vector3 _logicalPosition;

    [SerializeField] bool snap = true;


    public Vector3 LogicalPosition {
        get { return _logicalPosition; }
        set {
            _logicalPosition = value;
            if (snap) {
                var cellPosition = tilemap.WorldToCell(_logicalPosition);
                transform.position = tilemap.GetCellCenterWorld(cellPosition);
            } else {
                transform.position = _logicalPosition;
            }
        }
    }

    private void Start() {
        _logicalPosition = transform.position;
    }
}