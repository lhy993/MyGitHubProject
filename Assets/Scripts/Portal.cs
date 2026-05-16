using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Portal : MonoBehaviour
{
    public TMP_Text TIPTEXT;
    public int NeedLv;

    public UI_Battle Ui_Battle;

    void Update()
    {
    }
    void Start()
    {
        TIPTEXT.gameObject.SetActive(Shared.BattleMgr.playerInRange);
        if (Shared.BattleMgr.EnemyStage > 100)
        {
            TIPTEXT.text = ("포탈 버튼을 눌러 퇴장하기");
            NeedLv = 0;
        }
        else
        {
            NeedLv = Shared.BattleMgr.EnemyStage * 5;
            TIPTEXT.text = ("필요 레벨 " + NeedLv + "\n 포탈 버튼을 눌러 입장하기");
        }
    }
    public void PortalBtn()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (Shared.BattleMgr.EnemyStage > 100)
        {
            Shared.BattleMgr.EnemyStage -= 99;
            Ui_Battle.Reset();
}
        else if (NeedLv <= Shared.StatMgr.Lv)
        {
            Shared.BattleMgr.EnemyStage += 100;
            Ui_Battle.Reset();
        }   
    }      

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Shared.BattleMgr.playerInRange = true;
            TIPTEXT.gameObject.SetActive(true);
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Shared.BattleMgr.playerInRange = false;
            TIPTEXT.gameObject.SetActive(false);
        }
    }
}
