using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public float doorOpenAngle = 90f; 
    public float doorOpenSpeed = 5f; 
    public float detectionRange = 1.5f;

    private bool isDoorOpen = false;
    private Quaternion doorClosedRotation;
    private Quaternion doorOpenRotation;
    private BoxCollider doorCollider;
    private bool doorFullyOpen = false; 

    void Start()
    {
        doorClosedRotation = transform.rotation;
        doorOpenRotation = Quaternion.Euler(0, doorOpenAngle, 0);
        doorCollider = transform.GetChild(0).GetComponent<BoxCollider>(); 
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

        transform.rotation = Quaternion.Lerp(transform.rotation, doorOpenRotation, doorOpenSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, doorOpenRotation) < 1f) // Angle threshold
        {
            doorFullyOpen = true;
            doorCollider.enabled = false; 
        }
    }

    void CloseDoor()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, doorClosedRotation, doorOpenSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, doorClosedRotation) < 1f) 
        {
            doorFullyOpen = false;
            doorCollider.enabled = true;
        }
    }
}
