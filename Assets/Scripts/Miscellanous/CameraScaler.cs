using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
	Camera m_Camera;

	void Start ()
	{
		m_Camera = GetComponent<Camera>();
	}
	
	void Update ()
	{
		m_Camera.orthographicSize = 10;
	}
}
