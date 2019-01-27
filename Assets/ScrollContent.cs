using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollContent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Button button;
    void Start()
    {
        //var content = GetComponent<ScrollContent>();
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            //var path = SceneUtility.GetScenePathByBuildIndex(i);
            
            var scene = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            if (!scene.Contains("Level"))
            {
                continue;
            }
           Debug.Log($"{scene} at {i}");
            var button = Instantiate(this.button, transform, true);
            button.GetComponent<LevelLoader>().SceneIndex = i;
            button.GetComponentInChildren<Text>().text = scene;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
