using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] m_PossibleMusicSelection;
    private AudioSource m_AudioSource;

    public GameObject m_GameOverObject;

    void Start ()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.loop = true;

        int sel = Random.Range(0, m_PossibleMusicSelection.Length);
        m_AudioSource.clip = m_PossibleMusicSelection[sel];
        m_AudioSource.Play();

        
	}
	
	void Update ()
    {
        if (Player.sharedInstance.ReturnGameStatus())
        {
            m_AudioSource.Stop();
            m_GameOverObject.SetActive(true);
        }
	}
}
