using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public Rigidbody rigidbody;
    public UnityEvent collisionEvent;
    private int cacheRicochets;
    public int RicochetsLeft;
    public float MoveSpeed;

    public Vector3 velocity;

    public UnityEvent onDestroy;
    public BallAcceptor attachedAcceptor;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        cacheRicochets = RicochetsLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            DestroyThis();
        }

        transform.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        BallAcceptor hitAcceptor = collision.collider.GetComponent<BallAcceptor>();

        if ((!hitAcceptor || hitAcceptor.acceotedObject) && (collision.collider.CompareTag("Reflect")))
        {
            velocity = Vector3.Reflect(velocity, collision.GetContact(0).normal);
            RicochetsLeft -= 1;

            if (RicochetsLeft < 0)
            {
                DestroyThis();
            }
        }
        else if(hitAcceptor)
        {
            attachedAcceptor = hitAcceptor;
        }
        else
        {
            DestroyThis();
        }

        collisionEvent.Invoke();
    }

    public void ResetRicochets()
    {
        RicochetsLeft = cacheRicochets;
    }

    public void DestroyThis()
    {
        onDestroy.Invoke();

        if(attachedAcceptor)
        {
            attachedAcceptor.OnLoseBall();
        }

        GameObject.Destroy(this.gameObject);
    }
}
