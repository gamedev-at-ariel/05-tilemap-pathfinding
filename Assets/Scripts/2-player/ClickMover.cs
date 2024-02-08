using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component allows the player to move to a point in the screen by clicking it. 
 * Uses BFS to find the shortest path from the current location to the new location.
 */
public class ClickMover: TargetMover {

    [SerializeField] InputAction moveTo = new InputAction(type: InputActionType.Button);

    [SerializeField]
    [Tooltip("Determine the location to 'moveTo'.")]
    InputAction moveToLocation = new InputAction(type: InputActionType.Value, expectedControlType: "Vector2");

    void OnValidate() {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (moveTo.bindings.Count == 0)
            moveTo.AddBinding("<Mouse>/leftButton");
        if (moveToLocation.bindings.Count == 0)
            moveToLocation.AddBinding("<Mouse>/position");
    }

    private void OnEnable() {
        moveTo.Enable();
        moveToLocation.Enable();
    }

    private void OnDisable() {
        moveTo.Disable();
        moveToLocation.Disable();
    }

    void Update() {
        if (moveTo.WasPerformedThisFrame()) {
            Vector3 newTarget = Camera.main.ScreenToWorldPoint(moveToLocation.ReadValue<Vector2>());
            SetTarget(newTarget);
        }
    }
}
