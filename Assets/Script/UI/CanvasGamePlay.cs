using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class CanvasGamePlay : UICanvas
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    public void SettingButton()
    {

        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasSetting>();
        
    }
}
