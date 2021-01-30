using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
	public MenuManager manager;
	public string startSceneName = " ";
	public AudioClip startClip;
	public AudioClip optionClip;
	public AudioClip QuitClip;

	public AudioSource source;

	public void OnQuit()
	{
		source.PlayOneShot(QuitClip);
		Application.Quit();
	}

	public void OnStart()
	{
		source.PlayOneShot(startClip);
		SceneManager.LoadScene(startSceneName, LoadSceneMode.Single);
	}

	public void OnOptions()
	{
		source.PlayOneShot(optionClip);
		manager.SetGUIState(MenuManager.GUIState.OPTIONS);
	}
}
