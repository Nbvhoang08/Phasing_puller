using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class CanvasMainMenu : UICanvas
{
    // Start is called before the first frame update
   

    public void PlayerButton()
    {
        UIManager.Instance.CloseAll();
        SceneManager.LoadScene(1);
        UIManager.Instance.OpenUI<CanvasGamePlay>();

    }
    public void SettingButton()
    {
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
    public void ExitButton()
    {

    }

    // Update is called once per frame
}
