using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{

	AudioSource source;
	bool fadeIn = false;
	bool fadeOut = false;

	public float riseSpeed = 1;
	public float fallSpeed = 1;
	private void Start()
	{
		source = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (fadeIn)
		{
			if(!source.isPlaying)
			{
				source.Play();
			}
			source.volume += Time.deltaTime * riseSpeed;
			if (source.volume >= 1)
			{
				source.volume = 1;
				fadeIn = false;
			}
		}
		if (fadeOut)
		{
			source.volume -= Time.deltaTime * fallSpeed;
			if (source.volume <= 0)
			{
				source.volume = 0;
				fadeOut = false;
				source.Stop();
			}
		}
	}

	public void FadeIn()
	{
		fadeIn = true;
		fadeOut = false;
	}

	public void FadeOut()
	{
		fadeOut = true;
		fadeIn = false;
	}
}
