﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int SceneIndex;

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneIndex);
    }

}
