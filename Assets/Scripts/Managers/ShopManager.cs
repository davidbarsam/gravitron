using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour 
{
    // Public
    [Header("Objects")]
    public Text[] m_ShopCostTags;
    public GameObject[] m_DiamondButtons;

    // Private
    int m_CurrentCharacter;

	void Start () 
	{
        DebugFixMissingDefaultCharacter();
        UpdateSelections();
    }
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            PlayerPrefsManager.ResetUnlockedCharacters();
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            DebugReturnCurrentCharacter();
        }
	}

    public void PurchaseCharacter(int cost)
    {
        if(PlayerPrefsManager.GetMoneyAmount() >= cost)
        {
            PlayerPrefsManager.SpendMoneyAmount(cost);
            UnlockCharacter(cost / 1000);
            m_DiamondButtons[cost / 1000].SetActive(false);
        }
    }

    public void UnlockCharacter(int character)
    {
        PlayerPrefsManager.SetUnlockedCharacters(character.ToString());
        m_ShopCostTags[character].text = "Unlocked";
    }

    public void CheckIfUnlockedAndEnable(int character)
    {
        if (PlayerPrefsManager.GetUnlockedCharacters().Contains(character.ToString()))
        {
            PlayerPrefsManager.SetCurrentCharacter(character);
            Debug.Log("Current Character: " + PlayerPrefsManager.GetCurrentCharacter());
        }
        UpdateSelections();
    }

    public void UpdateSelections()
    {
        m_CurrentCharacter = PlayerPrefsManager.GetCurrentCharacter();
        for (int i = 0; i < m_ShopCostTags.Length; i++)
        {
            if (PlayerPrefsManager.GetUnlockedCharacters().Contains(i.ToString()))
            {
                m_DiamondButtons[i].SetActive(false);
                m_ShopCostTags[i].text = "Unlocked";
            }
        }
        m_ShopCostTags[m_CurrentCharacter].text = "Enabled";
    }

    public void DebugReturnCurrentCharacter()
    {
        Debug.Log("Current Character in Registry: " + PlayerPrefsManager.GetCurrentCharacter());
        Debug.Log("Current Unlocked Characters String: " + PlayerPrefsManager.GetUnlockedCharacters());
    }

    public void DebugFixMissingDefaultCharacter()
    {
        if (!PlayerPrefsManager.GetUnlockedCharacters().Contains("0"))
        {
            PlayerPrefsManager.SetUnlockedCharacters("0");
        }
    }
}
