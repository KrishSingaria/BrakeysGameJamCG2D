using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float anxiety = 0;
    [SerializeField] float Max_anxiety;
    Animator animator;
    public bool isDead = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isDead) 
        {
            DeadCondition();
        }
        
    }

    private void DeadCondition()
    {
        if (anxiety >= Max_anxiety)
        {
            animator.SetBool("dead", true);
            isDead = true;
            Destroy(FindAnyObjectByType<FirstPersonController>());
        }

        if (isDead)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Make sure the death animation is playing and that it has completed (normalizedTime >= 1.0)
            if (stateInfo.IsName("dead") && stateInfo.normalizedTime >= 1.0f)
            {
                animator.SetBool("dead", false);
                animator.Play("Idle");
            }
        }
    }
}
