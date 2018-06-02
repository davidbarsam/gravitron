using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSLimit : MonoBehaviour 
{

    public Toggle m_Toggle;

	void Start () 
    {
        if(GetComponent<Toggle>())
        {
            m_Toggle = GetComponent<Toggle>();
        }
        m_Toggle.isOn = PlayerPrefsManager.GetGlobalBatterySaver();
        OnValueChanged(PlayerPrefsManager.GetGlobalBatterySaver());
        m_Toggle.onValueChanged.AddListener(OnValueChanged);
	}
	
	void Update () 
    {
        
	}

    private void OnValueChanged(bool b)
    {
        PlayerPrefsManager.SetGlobalBatterySaver(b);
        switch(PlayerPrefsManager.GetGlobalBatterySaver())
        {
            case true:
                Application.targetFrameRate = 60;
                break;
            case false:
                Application.targetFrameRate = 30;
                break;
            default:
                Application.targetFrameRate = 60;
                break;
        }
    }
}
