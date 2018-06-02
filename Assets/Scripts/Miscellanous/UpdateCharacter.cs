using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCharacter : MonoBehaviour 
{
    public Sprite[] m_Characters;

	void Start () 
	{
        GetComponent<SpriteRenderer>().sprite = m_Characters[PlayerPrefsManager.GetCurrentCharacter()];
	}
	
	void Update () 
	{
		
	}
}
