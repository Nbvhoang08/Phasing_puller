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
            gameSettingCanvas.gameObject.SetActive(false);
        }
      
        
    }

    public void PlayerButton()
    {
        UIManager.Instance.CloseAll();
        SoundManager.Instance.PlayFxClicked();
        SenceController.Instance.ChangeScene("Scenes/Maps/Game_Play");
        UIManager.Instance.OpenUI<CanvasGamePlay>();

    }
    public void SettingButton()
    {
        if (gameSettingCanvas)
        {
            SoundManager.Instance.PlayFxClicked();
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
        SoundManager.Instance.PlayFxClicked();
        UIManager.Instance.QuitGame();
    }
    public void StoreBtn()
    {
        SoundManager.Instance.PlayFxClicked();
        UIManager.Instance.OpenUI<shopDialog>();
        UIManager.Instance.CloseUIDirectly<CanvasMainMenu>();
    }

    
}
