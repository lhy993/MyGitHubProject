using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour, iinteraction
{
    public UI_Battle Ui_Battle;
    public GameObject IMAGE;
    public TextMeshPro TIP;
    public Item reviveItem;
    public void ReturnBtn()
    {
        IMAGE.SetActive(false);
    }
    public void Text()
    {
        Ui_Battle.InteractionText("구매 하기");
    }
    public void Interact()
    {
        IMAGE.SetActive(true);
    }
    public void Tip()
    {
        TIP.text = $"골드를 사용하여 아이템을 구매할 수 있다";
        Inventory.instance.AddItem(reviveItem, 1);
    }
    public void ReviveBuy()
    {
        if (Shared.UserMgr.gold >= 50)
        {
            Shared.UserMgr.gold -= 50;
            Inventory.instance.AddItem(reviveItem, 1);
        }
    }


}
