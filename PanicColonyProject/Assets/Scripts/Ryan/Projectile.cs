using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public Rigidbody rigidbody;
    public int RicochetsLeft;
    public float MoveSpeed;

    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        velocity = Vector3.Reflect(velocity, collision.GetContact(0).normal);
        RicochetsLeft -= 1;

        if(RicochetsLeft < 0)
        {
            DestroyThis();
        }
    }

    public void DestroyThis()
    {
        GameObject.Destroy(this.gameObject);
    }
}
