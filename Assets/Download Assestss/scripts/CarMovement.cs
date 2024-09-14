using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 10f;        // Speed of the car
    public float turnSpeed = 50f;    // Turning speed
    public Transform[] waypoints;    // List of waypoints for the car to follow
    private int currentWaypointIndex = 0; // Index of the current waypoint

    void Update()
    {
        // Move the car forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Rotate towards the next waypoint
        Vector3 targetDirection = waypoints[currentWaypointIndex].position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Check if the car is close to the waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
