using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Portal : MonoBehaviour, iinteraction
{
    public int NeedLv;

    public UI_Battle Ui_Battle;
    public TextMeshPro TIP;

    public void Interact()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (Shared.BattleMgr.EnemyStage > 100)
        {
            Shared.BattleMgr.EnemyStage -= 100;
            Ui_Battle.Reset();
        }
        else if (NeedLv <= Shared.StatMgr.Lv)
        {
            Shared.BattleMgr.EnemyStage += 100;
            Ui_Battle.Reset();
        }
    }
    public void Text()
    {
        Ui_Battle.InteractionText("Æ÷Å» ÀÔÀå");
    }

    public void Tip()
    {
        if (Shared.BattleMgr.EnemyStage > 100)
        {
            TIP.text = "Exit";
            NeedLv = 0;
        }
        else
        {
            NeedLv = Shared.BattleMgr.EnemyStage * 5;
            TIP.text = $"Lv. {NeedLv} Required";
        }
    }
    public void TextOn()
    {
        TIP.gameObject.SetActive(true);
    }
    public void TextOff()
    {
        TIP.gameObject.SetActive(false);
    }
}
