using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{

    public static Player sharedInstance;

	// Variables
	SpriteRenderer m_SpriteRenderer;
	GameObject m_Player;

	bool m_GameOver;

	public float m_MoveSpeedVertical;
	public float m_MoveSpeedHoriz;

	public string m_BumperTag;
	public string m_ObstacleTag;

	private Renderer[] m_Renderers;
	private bool m_IsWrappingX;
	private bool m_IsWrappingY;

	void Start () 
	{
        sharedInstance = this;

        // Initialization
        m_Player = gameObject;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
		m_Renderers = GetComponentsInChildren<Renderer>();

		m_GameOver = false;
	}
	
	void Update () 
	{

		#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
		if(!m_GameOver)
		{
			if(Input.GetAxisRaw("Horizontal") > 0)
			{
				m_Player.transform.Translate(new Vector3(m_MoveSpeedHoriz * Time.deltaTime, 0));
			}
			else if(Input.GetAxisRaw("Horizontal") < 0)
			{
				m_Player.transform.Translate(new Vector3(-m_MoveSpeedHoriz * Time.deltaTime, 0));
			}
		}
		#endif

		#if UNITY_ANDROID || UNITY_IOS
		if(Input.touchCount > 0 && !m_GameOver)
        {
            // Store first touch detected.
            Touch[] myTouch = Input.touches;
            for(var i = 0; i < Input.touchCount; i++)
            {
                Vector2 touchPos = myTouch[i].position;
                if (touchPos.x < (Screen.width / 2))
                {
					m_Player.transform.Translate(new Vector3(-m_MoveSpeedHoriz * Time.deltaTime, 0));
                }
                else if (touchPos.x > (Screen.width / 2))
                {
					m_Player.transform.Translate(new Vector3(m_MoveSpeedHoriz * Time.deltaTime, 0));
				}
            }
        }
		#endif

		// Screen wrapper
		ScreenWrap();
		
	}

	void FixedUpdate()
	{
		if(!m_GameOver)
		{
			switch(m_SpriteRenderer.flipY)
			{
				case true:
					m_Player.transform.Translate(new Vector3(0, m_MoveSpeedVertical * Time.deltaTime));
					break;

				case false:
					m_Player.transform.Translate(new Vector3(0, -m_MoveSpeedVertical * Time.deltaTime));
					break;

				default:
					break;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag(m_BumperTag) && m_BumperTag != null)
		{
			m_SpriteRenderer.flipY = !m_SpriteRenderer.flipY;
		}
		if(collision.CompareTag(m_ObstacleTag) && m_ObstacleTag != null)
		{
			m_GameOver = true;
		}
	}

	void ScreenWrap()
	{
		var isVisible = CheckRenderers();
		if(isVisible)
		{
			m_IsWrappingX = false;
			return;
		}

		if(m_IsWrappingX)
		{
			return;
		}

		Vector3 newPosition = transform.position;
		if(newPosition.x > 1 || newPosition.x < 0)
		{
			newPosition.x = -newPosition.x;
			m_IsWrappingX = true;
		}

		transform.position = newPosition;

	}

	bool CheckRenderers()
	{
		foreach (Renderer render in m_Renderers)
		{
			if(render.isVisible)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		return false;
	}

    /// <summary>
    /// If true, then game is over. If false, then game is continuing.
    /// </summary>
    /// <returns>True if game is over.</returns>
	public bool ReturnGameStatus()
	{
		return m_GameOver;
	}
}