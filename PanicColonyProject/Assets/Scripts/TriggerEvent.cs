using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent trigEvent;
    public string trigTag = "Player";


	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == trigTag)
		{
			trigEvent.Invoke();
		}
	}

	public void TestEvent()
	{
		Debug.Log("Triggered");
	}
}
