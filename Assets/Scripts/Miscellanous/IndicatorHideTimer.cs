using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorHideTimer : MonoBehaviour {

	float timer = 0f;

	public float waitInSeconds = 3f;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if(gameObject.activeSelf == true)
		{
			timer += Time.deltaTime;
			if(timer >= waitInSeconds)
			{
				gameObject.SetActive(false);
				timer = 0;
			}

		}
	}
}
