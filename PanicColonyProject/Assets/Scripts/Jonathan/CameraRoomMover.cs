using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoomMover : MonoBehaviour
{
    public static CameraRoomMover instance;

    public int DegreesRotatedClockwise;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }


    private void LateUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E))
        {
            DegreesRotatedClockwise -= 1;

            if(DegreesRotatedClockwise < 0)
            {
                DegreesRotatedClockwise = 3;
            }
            transform.Rotate(new Vector3(0,-90,0));
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DegreesRotatedClockwise += 1;
            if (DegreesRotatedClockwise > 3)
            {
                DegreesRotatedClockwise = 0;
            }
            transform.Rotate(new Vector3(0,90,0));
        }

        fixedAge += Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, bounds.center + Vector3.up * bounds.extents.magnitude, fixedAge / secondsToReach);
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
