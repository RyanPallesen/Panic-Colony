using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelperArm : MonoBehaviour
{
    public GameObject ignoreObject;
    public Animator animator;
    public string trigger;
    private void OnTriggerEnter(Collider other)
    {
        Projectile proj = other.GetComponent<Projectile>();
        if (proj != null && proj.lastAttachedAI != ignoreObject)
        {
            animator.SetTrigger("Catch");
        }
    }
}
