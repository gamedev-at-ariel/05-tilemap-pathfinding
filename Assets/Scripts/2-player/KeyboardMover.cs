using UnityEngine;
using UnityEngine.InputSystem;


/**
 * This component allows the player to move by clicking the arrow keys.
 */
public class KeyboardMover : MonoBehaviour {

    [SerializeField] InputAction moveAction;

    void OnValidate() {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (moveAction == null)
            moveAction = new InputAction(type: InputActionType.Button);
        if (moveAction.bindings.Count == 0)
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow")
                .With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow")
                .With("Right", "<Keyboard>/rightArrow");
    }

    private void OnEnable() {
        moveAction.Enable();
    }

    private void OnDisable() {
        moveAction.Disable();
    }

    protected Vector3 NewPosition() {
        if (moveAction.WasPerformedThisFrame()) {
            Vector3 movement = moveAction.ReadValue<Vector2>(); // Implicitly convert Vector2 to Vector3, setting z=0.
            //Debug.Log("movement: " + movement);
            return transform.position + movement;
        } else {
            return transform.position;
        }
    }


    void Update()  {
        transform.position = NewPosition();
    }
}
