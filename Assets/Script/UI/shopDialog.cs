using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class shopDialog : UICanvas
{
    public Transform gridRoot;
    public TextMeshProUGUI CoinText;


    private void Awake()
    {
        UpdateUI();
        ShopManager.Instance.UpdateCoins(CoinText);
    }
    
    void ItemEvent(ShopItem item, int shopItemId, CanvasShopItems Pb)
    {
        if (item == null) return;
        SoundManager.Instance.PlayFxClicked();
        bool isUnLocked = Pref.GetBool(PrefConst.PLAYER_PEFIX + shopItemId);
        if (isUnLocked)
        {
            if (shopItemId == Pref.CurPlayerId) return;
            Pref.CurPlayerId = shopItemId;
            UpdateUI();
            ShopManager.Instance.UpdateCoins(CoinText);
            ShopManager.Instance.ActivePlayer();
        }
        else
        {
            // vật phẩm chưa đc mở khóa
            if (Pref.Coins >= item.price)
            {
                // nếu đủ tiền thì thực hiện mua
                Pref.Coins -= item.price;
                Debug.Log(Pref.Coins);
                Pref.SetBool(PrefConst.PLAYER_PEFIX + shopItemId, true);
                Pref.CurPlayerId = shopItemId;
                UpdateUI() ;
                ShopManager.Instance.UpdateCoins(CoinText);
                ShopManager.Instance.ActivePlayer();
            }
            else
            {
                // thông báo nếu ko đủ tiền
                Debug.Log("Nghèo");
            }
        }
    }

    private void UpdateUI()
    {
        var items = ShopManager.Instance.Items;
        if (items == null || items.Length <= 0 ||!gridRoot ) return;
        clearChild();
        for (int i = 0 ; i < items.Length; i++)
        {
            int idx = i; // lấy id của item trong Items 
            var item = items[i];
            if (item != null) 
            {
                CanvasShopItems  ItemsUIPb = UIManager.Instance.CreateNewUI<CanvasShopItems>(gridRoot);
                ItemsUIPb.UpdateUI(item, idx);
                if (ItemsUIPb.btn)
                {
                    ItemsUIPb.btn.onClick.RemoveAllListeners();
                    ItemsUIPb.btn.onClick.AddListener(() => ItemEvent(item, idx, ItemsUIPb)); //() là biểu tượng lamda để truyền sự kiện
                }
            }
            
        }
    }
    public void clearChild()
    {
        if (!gridRoot || gridRoot.childCount <= 0) return; 
        for(int i = 0; i < gridRoot.childCount; i++)
        {
            var child = gridRoot.GetChild(i);
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }
    }
    
    public void CloseBtn()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        UIManager.Instance.CloseUI<shopDialog>(0.1f);
        SoundManager.Instance.PlayFxClicked();
    }

}
