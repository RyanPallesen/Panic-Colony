using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    private float m_startingRotationX = 0.0f;

    private void Start()
    {
        m_startingRotationX = transform.eulerAngles.x;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(Vector3.right, -m_startingRotationX);
            transform.Rotate(Vector3.up, 90);
            transform.Rotate(Vector3.right, m_startingRotationX);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(Vector3.right, -m_startingRotationX);
            transform.Rotate(Vector3.up, -90);
            transform.Rotate(Vector3.right, m_startingRotationX);
        }
    }
}
