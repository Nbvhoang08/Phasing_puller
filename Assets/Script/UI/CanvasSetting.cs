using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class CanvasSetting : UICanvas
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] buttons;
    

  public void MainMenuButton()
  {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        SenceController.Instance.ChangeScene("Scenes/Menu");
  }
    public void ResumeButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
    public void CloseButton()
    {
        UIManager.Instance.QuitGame();
    }
}
