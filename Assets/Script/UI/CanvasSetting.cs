using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class CanvasSetting : UICanvas
{
    // Start is called before the first frame update

 
  public void MainMenuButton()
  {
        SenceController.Instance.ChangeScene("Scenes/Menu");
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        UIManager.Instance.ResumeGame();
    }
    public void ResumeButton()
    {
       
        UIManager.Instance.CloseAll();
        UIManager.Instance.ResumeGame();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        /* UIManager.Instance.CloseUIDirectly<CanvasSetting>();*/
        


    }
    public void CloseButton()
    {
        UIManager.Instance.QuitGame();
    }
}
