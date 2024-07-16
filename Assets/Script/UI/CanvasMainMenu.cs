using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using JetBrains.Annotations;

public class CanvasMainMenu : UICanvas
{
    // Start is called before the first frame update
    CanvasGameSetting gameSettingCanvas;
    public void Awake()
    {
        gameSettingCanvas = FindObjectOfType<CanvasGameSetting>();
        if (gameSettingCanvas != null && gameSettingCanvas.gameObject.activeInHierarchy)
        {
            // Xử lý khi tìm thấy và đối tượng đang hoạt động
            Debug.Log(" co");
            gameSettingCanvas.gameObject.SetActive(false);
        }
        else
        {
            // Xử lý khi không tìm thấy hoặc đối tượng không hoạt động
            Debug.Log(" ko");
           
        }
        
    }

    public void PlayerButton()
    {
        UIManager.Instance.CloseAll();
        SceneManager.LoadScene(1);
        UIManager.Instance.OpenUI<CanvasGamePlay>();

    }
    public void SettingButton()
    {
        if (gameSettingCanvas)
        {
            gameSettingCanvas.gameObject.SetActive(true);
            UIManager.Instance.CloseUIDirectly<CanvasMainMenu>();
        }
        else
        {
            Debug.LogWarning("null");
        }
       
    }
    public void ExitButton()
    {
        UIManager.Instance.QuitGame();
    }
    public void StoreBtn()
    {
        UIManager.Instance.OpenUI<shopDialog>();
        UIManager.Instance.CloseUIDirectly<CanvasMainMenu>();
    }

    
}
