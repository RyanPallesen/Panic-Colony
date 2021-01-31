using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    Transform[] allObjects;
    Vector3 cameraPosition;
    public BoxCollider collider;
    Bounds bound;

    

    // Start is called before the first frame update
    void Start()
    {
        allObjects = GetComponentsInChildren<Transform>();

        var Colliders = GetComponentsInChildren<Collider>();

        bound = new Bounds(Colliders[0].bounds.center, Vector3.zero);


        foreach (Collider collider in Colliders)
        {
            //if (collider.tag != "group" || collider.tag != "microlabel")
            {
                bound.Encapsulate(collider.bounds);
            }
        }

        bound.extents *= 0.9f;

        collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.center = bound.center - transform.position;
        collider.size = bound.extents * 2f;
        collider.gameObject.layer = 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerLocomotion>())
        {
            CameraRoomMover.instance.SetBounds(bound);
            other.GetComponentInChildren<ProjectileShooter>().shotProjectiles.Clear();
        }
    }

    private void OnTriggerExit(Collider other)
    {
     if(other.GetComponent<Projectile>())
        {
            Destroy(other.gameObject);
        }
    }
}
