using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShopItems : UICanvas
{
    public TextMeshProUGUI priceText;
    public Image hud;
    public Button btn;
  
    public void UpdateUI(ShopItem item, int shopItemId)
    {

        if (item == null) return;
        if (hud)
        {
            hud.sprite = item.hud;
        }
        bool isUnLockled = Pref.GetBool(PrefConst.PLAYER_PEFIX + shopItemId);
        Debug.Log(Pref.GetBool(PrefConst.PLAYER_PEFIX + shopItemId));
        if (isUnLockled)
        {
            
            Debug.Log("shopItemId "+shopItemId + " currentPlayerid" +Pref.CurPlayerId);
            if (shopItemId == Pref.CurPlayerId)
            {
               
                if (priceText)
                {
                    priceText.text = "Active";
                }
                
            }else
            {
                if (priceText)
                {
                    priceText.text = "Owned";
                }

            }
        }
        else
        {
          
            if (priceText)
            {
                priceText.text = item.price.ToString();
                
            }
        }
    }
}
