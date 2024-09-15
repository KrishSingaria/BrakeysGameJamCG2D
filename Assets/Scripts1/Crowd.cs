using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Crowd : MonoBehaviour
{
    // Start is called before the first frame update

    public NavMeshAgent agent;
    public GameObject Target;
    public GameObject[] AllTargets;
    void Start()
    {
        GetComponent<Animator>().SetInteger("Mode", 1);
        FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {
            if (Vector3.Distance(this.transform.position, Target.transform.position) <= 0.5f) 
            {
                FindTarget();
            }
        }
        
    }

    public void FindTarget() 
    {
        if(Target != null) 
        {
            Target.transform.tag = "Target";
        }
        AllTargets = GameObject.FindGameObjectsWithTag("Target");
        Target = AllTargets[Random.Range(0, AllTargets.Length)];
        Target.transform.tag = "Untagged";
        agent.destination = Target.transform.position;
    }
}
