using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component allows the player to move to a point in the screen by clicking it. 
 * Uses BFS to find the shortest path from the current location to the new location.
 */
public class ClickMover: TargetMover {

    [SerializeField] InputAction moveAction;

    void OnValidate() {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (moveAction == null)
            moveAction = new InputAction(type: InputActionType.Button);
        if (moveAction.bindings.Count == 0)
            moveAction.AddBinding("<Mouse>/leftButton");
    }

    private void OnEnable() {
        moveAction.Enable();
    }

    private void OnDisable() {
        moveAction.Disable();
    }

    void Update() {
        if (moveAction.WasPerformedThisFrame()) {
            Vector3 newTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SetTarget(newTarget);
        }
    }
}
