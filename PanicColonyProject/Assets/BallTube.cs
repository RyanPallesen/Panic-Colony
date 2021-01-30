using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTube : MonoBehaviour
{
    [SerializeField, Tooltip("The exit pipe")] private GameObject m_exitPipe = null;
    [SerializeField, Tooltip("The speed at which it leaves the pipe")] private float m_exitVelocity = 0.0f;
    [SerializeField, Tooltip("The offset at which the ball comes out")] private float m_exitOffset = 0.0f;

    private GameObject m_lastBall = null;

    private void OnTriggerEnter(Collider other)
    {
        Projectile projectile = other.GetComponent<Projectile>();

        if (projectile && other.gameObject != m_lastBall)
        {
            m_lastBall = other.gameObject;
            projectile.velocity = m_exitPipe.transform.forward * m_exitVelocity;
            projectile.transform.position = m_exitPipe.transform.position + (m_exitPipe.transform.forward * m_exitOffset);
        }
    }
}
