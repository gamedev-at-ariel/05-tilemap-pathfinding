using UnityEngine;

/**
 * This component allows the player to move by clicking the arrow keys.
 */
public class KeyboardMover : MonoBehaviour {
    void Update()  {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.position += Vector3.left;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.position += Vector3.right;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            transform.position += Vector3.down;
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            transform.position += Vector3.up;
        }
    }
}
