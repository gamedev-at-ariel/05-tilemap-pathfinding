using System.Collections.Generic;
using UnityEngine;

/**
 * This component patrols between given points, and chases a given target object when it sees it.
 */
public class PatrollerChaser: TargetMover {
    [SerializeField] Transform targetObject = null;
    [SerializeField] float radiusToWatch = 5f;
    [SerializeField] List<Transform> patrolPoints = null;
    [SerializeField] float gizmoRadius = 0.2f;

    private int currentPointIndex = 0;
    private void Update() {
        float distanceToTarget = Vector3.Distance(transform.position, targetObject.position);
        if (distanceToTarget <= radiusToWatch) {
            SetTarget(targetObject.position);
        } else {
            if (atTarget) {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Count;
            }
            SetTarget(patrolPoints[currentPointIndex].position);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        foreach (Transform point in patrolPoints) {
            Gizmos.DrawSphere(point.position, gizmoRadius);
        }
        Gizmos.DrawWireSphere(transform.position, radiusToWatch);
    }
}
 