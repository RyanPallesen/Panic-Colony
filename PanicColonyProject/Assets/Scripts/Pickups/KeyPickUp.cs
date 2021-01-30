using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public int keyNumber = 0;

    private void OnTriggerEnter(Collider other)
    {
        var loco = other.GetComponent<PlayerLocomotion>();

        if (loco != null)
        {
            PickupHandler.instance.OnKeyPicked(keyNumber, loco.transform);
            Destroy(this.gameObject);
        }
    }



}
