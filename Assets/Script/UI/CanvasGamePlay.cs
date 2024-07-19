using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;

public class CanvasGamePlay : UICanvas
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI CoinsText;



    public void Update()
    {
        CoinsText.text = Pref.Coins.ToString();
    }

    // Update is called once per frame
    public void SettingButton()
    {

        UIManager.Instance.CloseAll();
        UIManager.Instance.PauseGame();
        UIManager.Instance.OpenUI<CanvasSetting>(); 
    }

    




}
