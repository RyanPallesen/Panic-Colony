using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallButton : MonoBehaviour
{
    public GameObject acceptedObject;

    public UnityEvent onAcceptBall;
    public UnityEvent onLoseBall;

    public float timeForActivation;
    private float fixedAge;

    public bool isPressed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed == true)
        {
            fixedAge += Time.deltaTime;

            if (fixedAge > timeForActivation)
            {
                OnLoseBall();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.collider.GetComponent<Projectile>();

        Debug.Log("BallButton interacted with by " + collision.collider.gameObject.name);

        if (projectile)
        {
            fixedAge = 0f;
            isPressed = true;
            onAcceptBall.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile projectile = other.GetComponent<Projectile>();

        Debug.Log("BallButton interacted with by " + other.gameObject.name);

        if (projectile)
        {
            fixedAge = 0f;
            isPressed = true;
            onAcceptBall.Invoke();
        }
    }

    public void OnLoseBall()
    {
        isPressed = false;
        onLoseBall.Invoke();
    }

}
