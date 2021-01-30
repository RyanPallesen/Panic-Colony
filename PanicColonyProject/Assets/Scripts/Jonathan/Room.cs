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

        bound = new Bounds(Vector3.zero, Vector3.zero);
        var Colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in Colliders)
        {
            //if (collider.tag != "group" || collider.tag != "microlabel")
            {
                bound.Encapsulate(collider.bounds);
            }
        }

        collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.center = bound.center - transform.position;
        collider.size = bound.extents * 2f;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Trigger entered by " + other.name);

        if(other.GetComponent<PlayerLocomotion>())
        {
            CameraRoomMover.instance.SetBounds(bound);
        }
    }
}
