using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorAnimationHelper : MonoBehaviour
{
    public UnityEvent action;

    public void PlayerAnimation(Animation anim)
    {
        anim.Play();
    }
}
