using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("aUpgrade"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(2);
		}
		else if(Input.GetButtonDown("xUpgrade"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(3);
		}
		else if(Input.GetButtonDown("yUpgrade"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(4);
		}
		else if(Input.GetButtonDown("bUpgrade"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(5);
		}
		else if(Input.GetButtonDown("start_menu"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(6);
		}
		else if(Input.GetButtonDown("select_Reset"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(7);
		}
		else
		{
			//UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
    }
}
