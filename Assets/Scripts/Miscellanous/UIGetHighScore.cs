using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGetHighScore : MonoBehaviour {



	void Start () 
	{
		GetComponent<Text>().text = (PlayerPrefsManager.GetHighTimeMinutes() + ":" + PlayerPrefsManager.GetHighTimeSeconds() + ":" + Mathf.Round(PlayerPrefsManager.GetHighTimeMillisec()));
	}
	
	void Update () 
	{
		
	}
}
