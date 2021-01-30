using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorAnimationHelper : MonoBehaviour
{
    public bool isOpen;
    public float animationLength;

    public GameObject joint3;
    public GameObject joint5;

    private Vector3 closedPosJoint3;
    private Vector3 closePosJoint5;

    private float fixedAge;

    private void Start()
    {
        closedPosJoint3 = joint3.transform.position;
        closePosJoint5 = joint5.transform.position;
    }
    public void SetOpen(bool value)
    {
        isOpen = value;
        fixedAge = 0;

    }

    private void Update()
    {
        fixedAge += Time.deltaTime;

        if (isOpen)
        {
            joint3.transform.position = Vector3.Lerp(closedPosJoint3, closedPosJoint3 + transform.up * 0.5f, fixedAge / animationLength);
            joint5.transform.position = Vector3.Lerp(closePosJoint5, closePosJoint5 - transform.up * 4f, fixedAge / animationLength);
        }
        else
        {
            joint3.transform.position = Vector3.Lerp(closedPosJoint3 + transform.up * 0.5f, closedPosJoint3, fixedAge / animationLength);
            joint5.transform.position = Vector3.Lerp(closePosJoint5 - transform.up * 4f, closePosJoint5, fixedAge / animationLength);

        }
    }
}
