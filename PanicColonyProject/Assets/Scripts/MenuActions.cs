using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
	public string sceneName = " ";
	public void OnQuit()
	{
		Application.Quit();
	}

	public void OnStart()
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}
}
