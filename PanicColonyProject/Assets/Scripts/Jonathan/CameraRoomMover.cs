using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoomMover : MonoBehaviour
{
    public static CameraRoomMover instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        fixedAge += Time.deltaTime;

       transform.position = Vector3.Lerp(transform.position, bounds.center + Vector3.up * bounds.extents.magnitude + Vector3.back * bounds.extents.x/2, fixedAge / secondsToReach);
    }

    public void SetBounds(Bounds _bounds)
    {
        fixedAge = 0;
        bounds = _bounds;
    }

    public Bounds bounds;
    public float secondsToReach;
    private float fixedAge;
}
