using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    private Scene currentScene;
    void Start()
    {
       currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentScene.buildIndex);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(0);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        LoadNextScene();
    }

    public void LoadFirstPuzzle()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentScene.buildIndex + 1);

    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("Select");
    }

    public void ExitGame()
    {
        Application.Quit(0);
    }
}
