using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("start_menu"))
		{
			this.gameObject.GetComponent<SceneLoader>().LoadLevelSelect();
		}
		else if(Input.GetButtonDown("select_Reset"))
		{
			this.gameObject.GetComponent<SceneLoader>().ExitGame();
		}
		else if(Input.GetButtonDown("aUpgrade"))
		{
			this.gameObject.GetComponent<SceneLoader>().LoadFirstPuzzle();
		}
    }
}
