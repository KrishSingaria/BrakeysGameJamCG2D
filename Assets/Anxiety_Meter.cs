using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anxiety_Meter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Camera cam;
    private float anxiety;
    public float anxiety_dist = 5;
    public float anxiety_increaseing_rate = 1f;
    public float anxiety_decreaseing_rate = 0.1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dist = Vector3.Distance(player.transform.position, transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
        {
            print(hit.collider.name);
            if (hit.collider.name == "FirstPersonController" && dist < anxiety_dist && IsInView(cam))
            {
                anxiety += anxiety_increaseing_rate;
            }
            else if (anxiety > 0)
            {
                anxiety -= anxiety_decreaseing_rate;
            }
        }
        print(anxiety);
    }
    bool IsInView(Camera cam)
    {
        Vector3 viewportPosition = cam.WorldToViewportPoint(transform.position);

        // Check if the object is within the viewport
        bool isInView = viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
                        viewportPosition.y >= 0 && viewportPosition.y <= 1 &&
                        viewportPosition.z > 0;

        return isInView;
    }
}
