using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowLerp : MonoBehaviour 
{
    public AnimationCurve m_DetailCurve;
    public Shadow m_Shadow;
    public float m_Distance;
    public float m_WaitTimer;

    float timer = 0;
    float wTimer = 0;

	void Start () 
	{
		if(GetComponent<Shadow>())
        {
            m_Shadow = GetComponent<Shadow>();
        }
	}
	
	void Update () 
	{
        wTimer += Time.deltaTime;

        if(wTimer >= m_WaitTimer)
        {
            timer += Time.deltaTime;
            m_Shadow.effectDistance = new Vector2(Mathf.Lerp(0, m_Distance, m_DetailCurve.Evaluate(timer)), Mathf.Lerp(0, m_Distance, m_DetailCurve.Evaluate(timer)));
        }
	}
}
