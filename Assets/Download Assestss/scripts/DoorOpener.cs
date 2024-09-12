using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public float doorOpenAngle = 90f; // adjust this to your liking
    public float doorOpenSpeed = 5f; // adjust this to your liking
    public float detectionRange = 1.5f; // adjust this to your liking

    private bool isDoorOpen = false;
    private Quaternion doorClosedRotation;
    private Quaternion doorOpenRotation;
    private BoxCollider doorCollider;
    private bool doorFullyOpen = false; // Track if the door is fully open

    void Start()
    {
        doorClosedRotation = transform.rotation;
        doorOpenRotation = Quaternion.Euler(0, doorOpenAngle, 0);
        doorCollider = transform.GetChild(0).GetComponent<BoxCollider>(); // Get the Box Collider from child
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            isDoorOpen = true;
            OpenDoor();
        }
        else
        {
            isDoorOpen = false;
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        // Rotate the door towards the open rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, doorOpenRotation, doorOpenSpeed * Time.deltaTime);

        // Check if the door is fully open (rotation is approximately equal to the open rotation)
        if (Quaternion.Angle(transform.rotation, doorOpenRotation) < 1f) // Angle threshold
        {
            doorFullyOpen = true;
            doorCollider.enabled = false; // Disable the Box Collider when fully open
        }
    }

    void CloseDoor()
    {
        // Rotate the door towards the closed rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, doorClosedRotation, doorOpenSpeed * Time.deltaTime);

        // Enable the collider when the door is closed
        if (Quaternion.Angle(transform.rotation, doorClosedRotation) < 1f) // Angle threshold
        {
            doorFullyOpen = false;
            doorCollider.enabled = true; // Enable the Box Collider when fully closed
        }
    }
}
