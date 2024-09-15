using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public GameObject player; 
    public float activationDistance = 50f;

    private Collider objectCollider;

    void Start()
    {
        objectCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < activationDistance)
        {
            objectCollider.enabled = true; 
        }
        else
        {
            objectCollider.enabled = false; 
        }
    }
}
