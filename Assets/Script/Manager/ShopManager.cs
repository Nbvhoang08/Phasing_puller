using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    public ShopItem[] Items;
    Player player;
    protected override void Awake()
    {
        SetShouldNotDestroyOnLoad(false);
        base.Awake();
         
    }
    public void Start()
    {
        ActivePlayer();
        // nếu dưới máy người chơi chưa có dữ liệu coin thì setCoins =1000
        if(!PlayerPrefs.HasKey(PrefConst.COIN_KEY)) 
            Pref.Coins = 1000;
        if (Items == null || Items.Length <= 0) return;
        for (int i = 0; i < Items.Length; i++)
        {
            var item = Items[i];
            if(item != null)
            {
                if(i == 0)
                {
                    Pref.SetBool(PrefConst.PLAYER_PEFIX + i, true); //player_0
                    Pref.UpdateCurPlayerId(i);
                }
                else
                {
                    if (!PlayerPrefs.HasKey(PrefConst.PLAYER_PEFIX + i)) // kiem tra nguoi choi da luu nhan vat hay chua trong lan choi game dau tien
                    {                                                    
                        Pref.SetBool(PrefConst.PLAYER_PEFIX+i, false); 
                    }
                   
                }
            }
        }
    }
    public void UpdateCoins(TextMeshProUGUI CountingCoinsText) 
    {
        if (CountingCoinsText)
        {
            CountingCoinsText.text = Pref.Coins.ToString();
        }

    }
 


    public void ActivePlayer()
    {
        StartCoroutine(InstantiateNewPlayer());
    }

    private IEnumerator InstantiateNewPlayer()
    {
        // Hủy player hiện tại nếu có
        if (player != null)
        {
            Destroy(player.gameObject);
        }

        yield return null; // Chờ một frame để đảm bảo player cũ đã được hủy

        // Instantiate player mới nếu có
        var newPlayerPb = Items[Pref.CurPlayerId].playerPb;
        if (newPlayerPb)
        {
            player = Instantiate(newPlayerPb, Vector3.zero, Quaternion.identity);
        }
    }
}
[System.Serializable]
public class ShopItem
{
    public int price;
    public Sprite hud;
    public Player playerPb;
}
