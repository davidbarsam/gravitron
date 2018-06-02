using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {

	Scene m_Scene;
	public GameObject musicManager;
	//[HideInInspector]
	public GameObject childMusicManager;

    public Toggle m_MutedToggle;
    Toggle.ToggleEvent OnValueChanged;

    void Start () 
    {
        //Scene m_Scene = SceneManager.GetActiveScene();
        if(FindObjectOfType<MusicManager>() == null && SceneManager.GetActiveScene().buildIndex != 1)
        {
            Instantiate(musicManager as GameObject);
			childMusicManager = FindObjectOfType<MusicManager>().gameObject;
        }

        if(OnValueChanged == null && m_MutedToggle != null)
        {
            OnValueChanged = m_MutedToggle.onValueChanged;
	        OnValueChanged.AddListener(SetSourcesMuted);
        }
		if(m_MutedToggle != null)
		{
			m_MutedToggle.isOn = PlayerPrefsManager.GetGlobalMusicManagerMuted();
		}

    }
	
	void Update () 
	{
		if (childMusicManager == null && SceneManager.GetActiveScene().buildIndex != 1)
		{
			childMusicManager = FindObjectOfType<MusicManager>().gameObject;
		}
	}

	public void QuitGame()
	{
		Application.Quit();
	}

    public void myLoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

	public void myLoadSceneAsync(int index)
	{
		SceneManager.LoadSceneAsync(index);
	}

	public void ReturnToMenu(int index)
	{
		Time.timeScale = 1f;
		SceneManager.LoadSceneAsync(index);
	}

    void SetSourcesMuted(bool b)
    {
        PlayerPrefsManager.SetGlobalMusicManagerMuted(b);
        MusicManager.UpdateMuted(b);
    }

	public void PauseChildMusic()
	{
        if(childMusicManager != null)
        {
		    childMusicManager.GetComponent<MusicManager>().PauseMusic();

        }
	}

	public void UnpauseChild()
	{
        if(childMusicManager != null)
        {
		    childMusicManager.GetComponent<MusicManager>().UnPauseMusic();

        }
	}

    public void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}