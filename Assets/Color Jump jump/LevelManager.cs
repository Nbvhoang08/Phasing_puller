using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    // Start is called before the first frame update
    public List<string> sceneNames = new List<string>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (!sceneNames.Contains(currentSceneName))
        {
            sceneNames.Add(currentSceneName);
            Debug.Log("Saved current scene name: " + currentSceneName);
        }
    }
}
