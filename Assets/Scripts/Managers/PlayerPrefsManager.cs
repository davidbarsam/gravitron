using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

    // High Score variables
    const string HIGHEST_TIME_MINUTES = "highest_time_minutes";
	const string HIGHEST_TIME_SECONDS = "highest_time_seconds";
	const string HIGHEST_TIME_MILLISEC = "highest_time_millisec";

    // Global variables
    const int GLOBAL_MUSICMANAGER_MUTED = 0;
    const int GLOBAL_BATTERYSAVER = 1;

    // Currency variables
    const int MONEY_AMOUNT = 0;

    // Character variables
    const int CHARACTER_INDEX = 0;
    const string CHARACTER_UNLOCKED = "0";

    #region High Score Management

        /// <summary>
        /// Sets high score time's minutes
        /// </summary>
        /// <param name="minutes"></param>
	public static void SetHighTimeMinutes(float minutes)
	{
		if(minutes >= 0)
		{
			PlayerPrefs.SetFloat(HIGHEST_TIME_MINUTES, minutes);
		} else 
		{
			Debug.LogError("Recorded minutes out of range.");
		}
	}

        /// <summary>
        /// Returns high score time's minutes
        /// </summary>
        /// <returns></returns>
	public static float GetHighTimeMinutes()
	{
		return PlayerPrefs.GetFloat(HIGHEST_TIME_MINUTES, 0f);
	}

        /// <summary>
        /// Sets high score time's seconds
        /// </summary>
        /// <param name="seconds"></param>
	public static void SetHighTimeSeconds(float seconds)
	{
		if(seconds >= 0)
		{
			PlayerPrefs.SetFloat(HIGHEST_TIME_SECONDS, seconds);
		} else 
		{
			Debug.LogError("Recorded seconds out of range.");
		}
	}

        /// <summary>
        /// Gets high score time's seconds
        /// </summary>
        /// <returns></returns>
	public static float GetHighTimeSeconds()
	{
		return PlayerPrefs.GetFloat(HIGHEST_TIME_SECONDS, 0f);
	}

        /// <summary>
        /// Sets high score time's milliseconds
        /// </summary>
        /// <param name="milli"></param>
	public static void SetHighTimeMillisec(float milli)
	{
		if(milli >= 0)
		{
			PlayerPrefs.SetFloat(HIGHEST_TIME_MILLISEC, milli);
		} else
		{
			Debug.LogError("Recorded milliseconds out of range.");
		}
	}

        /// <summary>
        /// Gets high score time's milliseconds
        /// </summary>
        /// <returns></returns>
	public static float GetHighTimeMillisec()
	{
		return PlayerPrefs.GetFloat(HIGHEST_TIME_MILLISEC, 0f);
	}

    #endregion
    #region Global Options Manager

        /// <summary>>
    /// Sets the global variable MUSICMANAGER_MUTED, which handles muting the MusicManager, so nothing plays anywhere.
    /// </summary>
    public static void SetGlobalMusicManagerMuted(bool b)
    {
        switch(b)
        {
            case true:
                PlayerPrefs.SetInt("GLOBAL_MUSICMANAGER_MUTED", 1);
                break;
            case false:
                PlayerPrefs.SetInt("GLOBAL_MUSICMANAGER_MUTED", 0);
                break;

            default:
                PlayerPrefs.SetInt("GLOBAL_MUSICMANAGER_MUTED", 0);
                break;
        }
    }

        /// <summary>
    /// Returns state of MUSICMANAGER_MUTED, which handles playing music globally.
    /// </summary>
    /// <returns><c>true</c>, if global music manager muted was gotten, <c>false</c> otherwise.</returns>
    public static bool GetGlobalMusicManagerMuted()
    {
        switch(PlayerPrefs.GetInt("GLOBAL_MUSICMANAGER_MUTED"))
        {
            case 0:
                return false;
            case 1:
                return true;

            default:
                break;
        }

        return false;;

    }

        /// <summary>
        /// Sets state of Battery Saver
        /// </summary>
        /// <param name="b"></param>
    public static void SetGlobalBatterySaver(bool b)
    {
        switch(b)
        {
            case true:
                PlayerPrefs.SetInt("GLOBAL_BATTERYSAVER", 1);
                break;
            case false:
                PlayerPrefs.SetInt("GLOBAL_BATTERYSAVER", 0);
                break;
            default:
                break;
        }
    }

        /// <summary>
        ///  Gets state of Battery Saver
        /// </summary>
        /// <returns></returns>
    public static bool GetGlobalBatterySaver()
    {
        switch(PlayerPrefs.GetInt("GLOBAL_BATTERYSAVER"))
        {
            case 0:
                return false;
            case 1:
                return true;
            default:
                return true;
        }
    }

    #endregion
    #region Currency

        /// <summary>
    /// Sets total amount of money. Doesn't incriment.
    /// </summary>
    /// <param name="amt">Set total amount</param>
    public static void SetMoneyAmount(int amt)
    {
        PlayerPrefs.SetInt("MONEY_AMOUNT", amt);
    }

        /// <summary>
    /// Incriments money amount by value
    /// </summary>
    /// <param name="amt">How much to add to money amount</param>
    public static void IncrimentMoneyAmount(int amt)
    {
        int temp = PlayerPrefs.GetInt("MONEY_AMOUNT");
        temp += amt;
        PlayerPrefs.SetInt("MONEY_AMOUNT", temp);
    }
    
        /// <summary>
    /// Subtracts an amount from money amount
    /// </summary>
    /// <param name="amt">Amount to subtract</param>
    public static void SpendMoneyAmount(int amt)
    {
        int temp = PlayerPrefs.GetInt("MONEY_AMOUNT");
        temp -= amt;
        PlayerPrefs.SetInt("MONEY_AMOUNT", temp);
    }

        /// <summary>
    /// Returns total amount of money
    /// </summary>
    /// <returns>Total amount of money</returns>
    public static int GetMoneyAmount()
    {
        return PlayerPrefs.GetInt("MONEY_AMOUNT");
    }

    #endregion
    #region Characters

    /// <summary>
    /// Returns current character selection
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentCharacter()
    {
        return PlayerPrefs.GetInt("CHARACTER_INDEX");
    }

    /// <summary>
    /// Sets current character selection
    /// </summary>
    /// <param name="val"></param>
    public static void SetCurrentCharacter(int val)
    {
        PlayerPrefs.SetInt("CHARACTER_INDEX", val);
    }

    /// <summary>
    /// Returns unlocked characters
    /// </summary>
    /// <returns></returns>
    public static string GetUnlockedCharacters()
    {
        return PlayerPrefs.GetString("CHARACTER_UNLOCKED");
    }

    /// <summary>
    /// Sets "list" of unlocked characters
    /// </summary>
    /// <param name="val"></param>
    public static void SetUnlockedCharacters(string val)
    {
        PlayerPrefs.SetString("CHARACTER_UNLOCKED", PlayerPrefs.GetString("CHARACTER_UNLOCKED") + val);
    }

    /// <summary>
    /// Resets value of unlocked characters
    /// </summary>
    public static void ResetUnlockedCharacters()
    {
        PlayerPrefs.SetString("CHARACTER_UNLOCKED", "0");
    }

    #endregion

}