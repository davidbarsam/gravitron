using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

	bool isPaused;
	public AudioClip pauseSound;

	void Start ()
	{
		isPaused = false;
	}
	
	void Update ()
	{
		if(isPaused)
		{
			Time.timeScale = 0;
		} else
		{
			Time.timeScale = 1;
		}
	}

	public void TogglePause()
	{
		isPaused = !isPaused;
	}

	public void IsReturningToMenu()
	{
		isPaused = false;
	}
}
