using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        PlayMusic();
       
        
    }

    public void PlayMusic()
    {
        if (source.isPlaying) return;
        
        source.loop = true;
        source.Play();
        
    }

    public void StopMusic()
    {
        source.Stop();
    }
}
