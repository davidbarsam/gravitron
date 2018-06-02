using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAudioSource : MonoBehaviour 
{

	void Start () 
    {
        GetComponent<AudioSource>().mute = PlayerPrefsManager.GetGlobalMusicManagerMuted();
	}
	
	void Update () 
    {
		
	}
}
