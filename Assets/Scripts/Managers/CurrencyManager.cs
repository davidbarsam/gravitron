using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurrencyManager : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("Amount to award player after they complete an ad.")] public int m_GemsFromAd = 45;

    [Header("Objects")]
    public Text[] m_MoneyDisplay;
    public Text m_EarnedDisplay;

    public GameObject m_PostAdPanel;
    public Text m_PostAdPanelTitle;
    public Text m_PostAdPanelDesc;

    public GameObject m_WaitingAdPanel;

    [HideInInspector]public CurrencyManager sharedInstance;

    private int m_AmountToAward = 0;

    private void OnEnable()
    {
        sharedInstance = this;

        Debug.Log("CM: CurrencyManager is enabled!");
        foreach(Text txt in m_MoneyDisplay)
        {
            txt.text = PlayerPrefsManager.GetMoneyAmount().ToString();
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("CM: Current scene is Game! Awarding points...");
            CalculateMoney();
            AwardMoney(m_AmountToAward);
            foreach (Text txt in m_MoneyDisplay)
            {
                txt.text = PlayerPrefsManager.GetMoneyAmount().ToString();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.End) || Input.GetKeyDown(KeyCode.F2))
        {
            PlayerPrefsManager.SetMoneyAmount(0);
            Debug.Log("CM: Money reset to 0!");
            foreach (Text txt in m_MoneyDisplay)
            {
                txt.text = PlayerPrefsManager.GetMoneyAmount().ToString();
            }
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.F1))
        {
            PlayerPrefsManager.IncrimentMoneyAmount(1000);
            Debug.Log("CM: Added 1000 money!");
            foreach(Text txt in m_MoneyDisplay)
            {
                txt.text = PlayerPrefsManager.GetMoneyAmount().ToString();
            }
        }
    }

    void CalculateMoney()
    {
        // pts are proportional to seconds. so every second you get 1 pt
        // that means every minute you get 60 pts

        // every second add 1 pt
        m_AmountToAward += Mathf.RoundToInt(Timer.sharedInstance.Seconds);

        // every minute add 60 pts
        m_AmountToAward += Mathf.RoundToInt(Timer.sharedInstance.Minutes * 60);

        m_EarnedDisplay.text = ("+" + m_AmountToAward.ToString());
    }

    void AwardMoney(int amt)
    {
        PlayerPrefsManager.IncrimentMoneyAmount(amt);
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
            
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                m_WaitingAdPanel.SetActive(false);
                PlayerPrefsManager.IncrimentMoneyAmount(m_GemsFromAd);
                foreach (Text txt in m_MoneyDisplay)
                {
                    txt.text = PlayerPrefsManager.GetMoneyAmount().ToString();
                }
                m_PostAdPanel.SetActive(true);
                m_PostAdPanelTitle.text = "Thanks for supporting me!";
                m_PostAdPanelDesc.text = "You earned " + m_GemsFromAd + " gems!";
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                m_PostAdPanel.SetActive(true);
                m_PostAdPanelTitle.text = "You skipped the ad.";
                m_PostAdPanelDesc.text = "You don't get anything for skipping the ad.";
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                m_PostAdPanel.SetActive(true);
                m_PostAdPanelTitle.text = "There was an error.";
                m_PostAdPanelDesc.text = "Try again later.";
                break;
        }
    }

    public void UpdateMoneyDisplays()
    {
        foreach(Text txt in m_MoneyDisplay)
        {
            txt.text = PlayerPrefsManager.GetMoneyAmount().ToString();
        }
    }
}