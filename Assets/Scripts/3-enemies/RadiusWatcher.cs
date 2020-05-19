using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component makes its object watch a given radius, and if the target is found - it starts chasing it.
 */
[RequireComponent(typeof(Chaser))]
public class RadiusWatcher: MonoBehaviour {
    [SerializeField] Transform target = null;
    [SerializeField] float radiusToWatch = 5f;

    private Chaser chaser;
    private void Start() {
        chaser = GetComponent<Chaser>();
    }

    void Update() {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= radiusToWatch) {
            chaser.enabled = true;
        } else {
            chaser.enabled = false;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusToWatch);
    }
}
