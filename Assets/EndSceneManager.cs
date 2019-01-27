using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("start_menu") || Input.GetButtonDown("aUpgrade"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
		else if(Input.GetButtonDown("select_Reset"))
		{
			this.gameObject.GetComponent<SceneLoader>().ExitGame();
		}
    }
}
