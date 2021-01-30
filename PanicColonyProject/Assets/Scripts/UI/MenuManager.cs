using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject main;
    public GameObject options;
    // Start is called before the first frame update
    void Start()
    {
        SetGUIState(GUIState.MENU);
    }
    
    public void SetGUIState(GUIState state)
	{
		switch (state)
		{
            case GUIState.MENU:
                main.SetActive(true);
                options.SetActive(false);
                break;
            case GUIState.OPTIONS:
                main.SetActive(false);
                options.SetActive(true);
                break;
        }

	}
	public enum GUIState
	{ 
        MENU,
        OPTIONS
    }



}
