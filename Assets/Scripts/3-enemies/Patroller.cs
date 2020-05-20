using System.Collections.Generic;
using UnityEngine;

/**
 * This component patrols between given points.
 */
public class Patroller: TargetMover {
    [SerializeField] List<Transform> patrolPoints = null;
    [SerializeField] float gizmoRadius = 0.2f;  // for gizmos of patrol points

    private int currentPointIndex = 0;
    private void Update() {
        if (atTarget) {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Count;
        }
        SetTarget(patrolPoints[currentPointIndex].position);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        for (int i=0; i<patrolPoints.Count; ++i) {
            Gizmos.DrawSphere(patrolPoints[i].position, gizmoRadius);
            if (i > 0)
                Gizmos.DrawLine(patrolPoints[i - 1].position, patrolPoints[i].position);
        }
    }
}
 