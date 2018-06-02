using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    // VAR
	AudioSource m_AudioSource;  // Var storing object's Audio Source.
	[Header("List of Music Files")] [Tooltip("List corresponds to current scene. Arrange music files accordingly.")]
	public AudioClip[] m_MusicSources;  // Array of audio clips, editable in the Inspector.
	Scene m_currentScene;   // Var storing current scene
	[Header("Possible Game Music Selection")]
	public AudioClip[] m_GameMusicSources;

	AudioClip m_tempAudioClip;
	int randomMusic;

	void Start () 
	{
        // Register the gameObject's Audio Source...
		m_AudioSource = GetComponent<AudioSource>();
        // Register the gameObject's scene...
		m_currentScene = SceneManager.GetActiveScene();
        // Subscribe to the activeSceneChanged delegate.
		SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;


	}

    /// <summary>
    /// This delegate listens for SceneManager's activeSceneChanged event, which notifies us when the scene changes.
    /// </summary>
    /// <param name="arg0">Arg0.</param>
    /// <param name="arg1">Arg1.</param>
	private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
	{
        Debug.Log("Scene changed");
        // Unpause the player.
        if(m_AudioSource != null)
        {
		    m_AudioSource.UnPause();

        }
		//throw new System.NotImplementedException();
	}

	void Update () 
	{

		// If a scene changes...
		if (SceneManager.GetActiveScene() != m_currentScene)
		{
			randomMusic = Random.Range(0, m_GameMusicSources.Length);
			Debug.Log(randomMusic);
			// Update our variable...
			m_currentScene = SceneManager.GetActiveScene();
            // Unpause the player...
			m_AudioSource.UnPause();
            // Play the loaded clip.
			m_AudioSource.Play();
		}

		if(SceneManager.GetActiveScene().buildIndex == 1)
		{
			
			Debug.Log(m_GameMusicSources.Length);
			if(m_GameMusicSources[m_currentScene.buildIndex] != null)
			{
                AudioClip clip = m_GameMusicSources[randomMusic];
                Debug.Log(clip);
                m_AudioSource.clip = clip;
            }
            m_AudioSource.Play();
		}
		else if (m_MusicSources[m_currentScene.buildIndex] != null)
			{

			// If there is a clip in the array to match the current scene's build index...
				// Check if array has any clips
			{
				// If there's no clip loaded OR if the current clip's name doens't match the clip's name in the array matching the scene's index...
				if(m_AudioSource.clip == null || m_AudioSource.clip.name != m_MusicSources[m_currentScene.buildIndex].name)
				{
					// Change the clip to one that matches the current scene's build index.
					m_AudioSource.clip = m_MusicSources[m_currentScene.buildIndex];
					// Play the correct clip.
					m_AudioSource.Play();
				}
			}

		}

        // Dynamically change muted status
        //UpdateMuted(CheckMutedStatus());
	}

	//public void InterruptAudio(AudioClip ac)
	//{
		
	//}

    bool CheckMutedStatus()
    {
        return PlayerPrefsManager.GetGlobalMusicManagerMuted();
    }

    public static void UpdateMuted(bool muted)
    {
        AudioSource[] a = FindObjectsOfType<AudioSource>();
        switch (muted)
        {
            case true:
                for (int i = 0; i < a.Length; i++)
                {
                    a[i].mute = true;
                }
                break;
            case false:
                for (int i = 0; i < a.Length; i++)
                {
                    a[i].mute = false;
                }
                break;
            default:
                break;
        }
    }

	public void PauseMusic()
	{
		FindObjectOfType<AudioSource>().Pause();
		Debug.Log("Paused music");
	}

	public void UnPauseMusic()
	{
		FindObjectOfType<AudioSource>().UnPause();
		Debug.Log("Un paused music");
	}

	public void PauseMusicAndStore()
	{
		m_tempAudioClip = m_AudioSource.clip;
		m_AudioSource.Pause();
	}

	public void ResumeStoredClip()
	{
		m_AudioSource.clip = m_tempAudioClip;
		m_AudioSource.UnPause();
	}
}