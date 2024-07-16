using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pref 
{
   
    public static int CurPlayerId
    {
        set => PlayerPrefs.SetInt(PrefConst.CUR_PLAYER_ID, value);
        get => PlayerPrefs.GetInt(PrefConst.CUR_PLAYER_ID);
    }
    public static int Coins
    {
        set => PlayerPrefs.SetInt(PrefConst.COIN_KEY, value);
        get => PlayerPrefs.GetInt(PrefConst.COIN_KEY);
    }
    private void Start()
    {
        PlayerPrefs.SetInt("coin", 1000);
        PlayerPrefs.GetInt("coin");

    }
    public static void SetBool(string key, bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt(key, 1); // luu xuong may nguoi dung voi gia tri 1
        }
        else
        {
            PlayerPrefs.SetInt(key, 0); // luu xuong may nguoi dung voi gia tri 1
        }
    }
    public static void UpdateCurPlayerId(int newPlayerId)
    {
        Pref.CurPlayerId = newPlayerId;
        Debug.Log("CurPlayerId đã được cập nhật thành: " + Pref.CurPlayerId);
    }

    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1 ? true : false;
        // tra ve true neu key =1 , false neu key = 0
    }

}
