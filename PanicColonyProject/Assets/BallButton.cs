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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fixedAge += Time.deltaTime;

        if (fixedAge > timeForActivation)
        {
            OnLoseBall();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.collider.GetComponent<Projectile>();

        if (projectile)
        {
            fixedAge = 0f;
            onAcceptBall.Invoke();
        }
    }

    public void OnLoseBall()
    {
        onLoseBall.Invoke();
    }

}
