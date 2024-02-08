using UnityEngine;
using UnityEngine.UIElements;

/**
 * This component just rotates its object between angular bounds.
 */
public class Rotator : MonoBehaviour {
    [SerializeField] float minAngle = -90;
    [SerializeField] float maxAngle = 90;
    [SerializeField] Vector3 angularVelocity= new Vector3(30,0,0);

    private int direction = 1;
    private float angle = 0;

    void Update() {
        transform.Rotate(direction* angularVelocity*Time.deltaTime);
        angle += direction * Time.deltaTime;
        if (angle > 180)
            angle -= 360;
        if (angle <= minAngle)
            direction = 1;
        if (angle >= maxAngle)
            direction = -1;
    }
}
