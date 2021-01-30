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
