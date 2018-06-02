using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class ImageFade : MonoBehaviour
{
	[Header("OBJECT MUST HAVE A CANVAS GROUP COMPONENT")]
	public CanvasGroup m_CanvasGroup;
	[Space(10f)]

	[Header("Options")]
	public bool fadeAtStart;
	public bool fadeAtTime;
	public float waitForTime;
	public bool fadeAtEnd;
	private bool isEnd;
	[Space(10f)]

	[Header("Properties")]
	public AnimationCurve fadeTimeline;
	public float fadeSpeed;

	float internalTimer = 0;
	bool wasAtStart;
	bool wasAtTime;

	public UnityEvent onFinishFading;

	void Start ()
	{
		if (GetComponent<CanvasGroup>())
		{
			m_CanvasGroup = GetComponent<CanvasGroup>();
			m_CanvasGroup.alpha = 0;
		}
		if(onFinishFading == null)
		{
			onFinishFading = new UnityEvent();
		}
		internalTimer = 0;
		isEnd = false;
		wasAtStart = fadeAtStart;
		wasAtTime = fadeAtTime;
	}
	
	void Update ()
	{
		if(gameObject.activeInHierarchy)
		{
			if(fadeAtStart && fadeAtTime)
			{
				Debug.LogError("Cannot select multiple options. Choose one option.");
				gameObject.SetActive(false);
				throw new System.Exception("You cannot select multiple options. Choose one option only.");
			}
			else if(fadeAtStart)
			{
				if(m_CanvasGroup.alpha != 1)
				{
					if(Time.timeScale == 1)
					{
						m_CanvasGroup.alpha = fadeTimeline.Evaluate(internalTimer += Time.deltaTime * fadeSpeed);
					} else if(Time.timeScale == 0)
					{
						m_CanvasGroup.alpha = fadeTimeline.Evaluate(internalTimer += Time.unscaledDeltaTime * fadeSpeed);
					}
				} else if(m_CanvasGroup.alpha == 1)
				{
					fadeAtStart = false;
					internalTimer = 0;
					onFinishFading.Invoke();
				}
			}
			else if(fadeAtEnd && isEnd)
			{
				if(m_CanvasGroup.alpha != 0)
				{
					m_CanvasGroup.alpha = fadeTimeline.Evaluate(internalTimer -= Time.unscaledDeltaTime * fadeSpeed);
				}
				else if(m_CanvasGroup.alpha == 0)
				{
					Debug.Log("internal timer resetting, fadeAtEnd done and is false.");
					fadeAtEnd = false;
					internalTimer = 0;
					isEnd = false;
					onFinishFading.Invoke();
				}
			}
		}
	}

	public void InvokeEnding()
	{
		isEnd = true;
		internalTimer = 1;
	}

	public void ResetConditions()
	{
		fadeAtStart = wasAtStart;
		fadeAtTime = wasAtTime;
	}
}
