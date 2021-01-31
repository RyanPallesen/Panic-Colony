using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTransform : MonoBehaviour
{
    public Transform transformToFollow;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transformToFollow)
        {
            transform.rotation = Quaternion.LookRotation(transformToFollow.transform.position - transform.position) * Quaternion.Euler(0, 180, 0);
        }
    }
}
