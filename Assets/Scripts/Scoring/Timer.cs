using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer sharedInstance;

	public Text m_TimerLabel;

	float m_Milliseconds;
	float m_Seconds;
	float m_Minutes;
	float m_FormattedTime;

	public bool m_IsPaused { get { return isPaused; } set { isPaused = value; } }
	bool isPaused;

    public float Milliseconds { get { return m_Milliseconds; } set { m_Milliseconds = value; } }
    public float Seconds { get { return m_Seconds; } set { m_Seconds = value; } }
    public float Minutes { get { return m_Minutes; } set { m_Minutes = value; } }

    public GameObject m_gameOverPanel;
	public Text m_gameOverTimer;
    public GameObject m_CurrencyManager;

	void Start ()
	{
        sharedInstance = this;

		m_Milliseconds = 0;
		m_Seconds = 0;
		m_Minutes = 0;
	}
	
	void Update ()
	{
		if(!FindObjectOfType<Player>().ReturnGameStatus())
		{
			if(!m_IsPaused)
			{
				m_Milliseconds += Time.deltaTime * 100;
				if(m_Milliseconds >= 100)
				{
					m_Seconds++;
					m_Milliseconds = 0;
				}
				if(m_Seconds >= 60)
				{
					m_Minutes++;
					m_Seconds = 0;
				}
				m_TimerLabel.text = (m_Minutes + ":" + m_Seconds + "." + Mathf.Round(m_Milliseconds));
			}
		} else if (FindObjectOfType<Player>().ReturnGameStatus())
		{
			RecordScore();
			m_gameOverTimer.text = (m_Minutes + ":" + m_Seconds + "." + Mathf.Round(m_Milliseconds));
			m_gameOverPanel.SetActive(true);
            m_CurrencyManager.SetActive(true);
		}
	}

/// <summary>
/// Records time by each segment: Minutes, seconds, and milliseconds.
/// If a segment is identical to a recorded segment, it skips it.
/// If a segment is greater than a recorded segment, it overwrites it.
/// If a segment is smaller than a recorded segment, it skips it.
/// </summary>
	void RecordScore()
	{
		if(PlayerPrefsManager.GetHighTimeMinutes() == m_Minutes)
		{
			if(PlayerPrefsManager.GetHighTimeSeconds() == m_Seconds)
			{
				if(PlayerPrefsManager.GetHighTimeMillisec() == m_Milliseconds)
				{
					Debug.Log("Current time and high time are exactly the same. Somehow.");
					return;
				} else if(PlayerPrefsManager.GetHighTimeMillisec() < m_Milliseconds)
				{
					Debug.Log("Recording high score milliseconds.");
					PlayerPrefsManager.SetHighTimeMillisec(Mathf.Round(m_Milliseconds));
					return;
				}
			} else if (PlayerPrefsManager.GetHighTimeSeconds() < m_Seconds)
			{
				Debug.Log("Recording high score seconds and milliseconds.");
				PlayerPrefsManager.SetHighTimeSeconds(m_Seconds);
				PlayerPrefsManager.SetHighTimeMillisec(m_Milliseconds);
				return;
			}
		} else if (PlayerPrefsManager.GetHighTimeMinutes() < m_Minutes)
		{
			Debug.Log("Recording high score minutes, seconds, milliseconds.");
			PlayerPrefsManager.SetHighTimeMinutes(m_Minutes);
			PlayerPrefsManager.SetHighTimeSeconds(m_Seconds);
			PlayerPrefsManager.SetHighTimeMillisec(m_Milliseconds);
			return;
		}
	}

	
}
