using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Portal : MonoBehaviour
{
    public TMP_Text TIPTEXT;
    public int NeedLv;
    public GameObject PLAYER;
    
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

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        if (Shared.BattleMgr.EnemyStage > 100)
        {
            Shared.BattleMgr.PortalReset = true;
            Shared.BattleMgr.EnemyStage -= 99;
            Shared.BattleMgr.enemyCount = 0;
            PLAYER.transform.position = new Vector3(-15f, -3f, 0f);
        }
        else if (NeedLv <= Shared.StatMgr.Lv)
        {
            Shared.BattleMgr.PortalReset = true;
            Shared.BattleMgr.life = 3;
            Shared.BattleMgr.EnemyStage += 100;
            PLAYER.transform.position = new Vector3(-15f, -3f, 0f);
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
