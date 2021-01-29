using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent trigEnterEvent;
	public UnityEvent trigExitEvent;
	public string trigTag = "Player";


	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == trigTag)
		{
			trigEnterEvent.Invoke();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == trigTag)
		{
			trigExitEvent.Invoke();
		}
	}

	public void TestEvent()
	{
		Debug.Log("Triggered");
	}
}
