﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStopper : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {

        GameObject.FindGameObjectWithTag("Music")?.GetComponent<MusicManager>().StopMusic();
    }

    public void RestartMusicPlease()
    {
        GameObject.FindGameObjectWithTag("Music")?.GetComponent<MusicManager>().PlayMusic();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
