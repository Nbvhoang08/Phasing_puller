using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevel : Singleton<TestLevel>
{
    // Start is called before the first frame update
    public LevelCanvas level;
    void Start()
    {
        level.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
           level.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            level.gameObject.SetActive(false);
        }
        // Chuyển sang scene kế tiếp khi nhấn nút R
        if (Input.GetKeyDown(KeyCode.R))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(nextSceneIndex);
        }

        // Quay lại scene trước đó khi nhấn nút T
        if (Input.GetKeyDown(KeyCode.W))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int previousSceneIndex = currentSceneIndex - 1;
            if (previousSceneIndex < 0)
            {
                previousSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
            }
            SceneManager.LoadScene(previousSceneIndex);
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            LevelManager.Instance.SaveGame();
        }
    }
}
