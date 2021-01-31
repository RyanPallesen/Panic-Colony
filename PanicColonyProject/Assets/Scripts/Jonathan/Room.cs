using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    Transform[] allObjects;
    Vector3 cameraPosition;
    public BoxCollider collider;
    Bounds bound;

    private Vector3 respawnPosition;
    private GameObject playerObject;

    public bool isCurrentRoom;
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

    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.R) && isCurrentRoom)
        {
            playerObject.transform.position = respawnPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerLocomotion>())
        {
            isCurrentRoom = true;
               playerObject = other.gameObject;
            CameraRoomMover.instance.SetBounds(bound);
            respawnPosition = other.transform.position;
            other.GetComponentInChildren<ProjectileShooter>().shotProjectiles.Clear();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Projectile>())
        {
            other.GetComponent<Projectile>().DestroyThis();
        }
        else if(other.GetComponent<PlayerLocomotion>())
        {
            isCurrentRoom = false;
        }
    }


}
