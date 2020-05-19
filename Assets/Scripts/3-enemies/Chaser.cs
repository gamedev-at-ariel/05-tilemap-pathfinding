using UnityEngine;

/**
 * This component chases a given target object.
 */
public class Chaser: TargetMover {
    [Tooltip("The object that we try to chase")]
    [SerializeField] Transform targetObject = null;

    private void Update() {
        SetTarget(targetObject.position);
    }
}
