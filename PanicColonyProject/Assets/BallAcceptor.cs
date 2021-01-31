using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallAcceptor : MonoBehaviour
{
    public GameObject acceotedObject;

    public UnityEvent onAcceptBall;
    public UnityEvent onLoseBall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.collider.GetComponent<Projectile>();

        if(projectile && !acceotedObject)
        {
            projectile.velocity = Vector3.zero;
            projectile.GetComponent<Collider>().enabled = false;
            onAcceptBall.Invoke();
            acceotedObject = projectile.gameObject;
        }
    }

    public void OnLoseBall()
    {
        onLoseBall.Invoke();

        acceotedObject = null;
    }
    
}
