using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Portal : MonoBehaviour
{
    public TMP_Text TIPTEXT;
    public int NeedLv;
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
        switch (Shared.BattleMgr.EnemyStage)
        {
            case 1://고블린
                {
                    Shared.BattleMgr.life = 3;
                    PortalLv(SCENE.Battle_Boss_Goblin);
                    break;
                }
            case 101://고블린 보스
                {
                    Shared.BattleMgr.enemyCount = 0;
                    PortalLv(SCENE.Battle_Goblin);
                    break;
                }
            case 2://스켈레톤 
                {
                    Shared.BattleMgr.life = 3;
                    PortalLv(SCENE.Battle_Boss_Skeleton);
                    break;
                }
            case 102://스켈레톤 보스
                {
                    Shared.BattleMgr.enemyCount = 0;
                    PortalLv(SCENE.Battle_Skeleton);
                    break;
                }
            case 3://트렁크
                {
                    PortalLv(SCENE.Battle_Boss_Trunk);
                    break;
                }
            case 103://트렁크 보스
                {
                    Shared.BattleMgr.enemyCount = 0;
                    Shared.BattleMgr.life = 3;
                    PortalLv(SCENE.Battle_Trunk);
                    break;
                }
        }
    }
    public void PortalLv(SCENE scene)
    {
        if (Shared.BattleMgr.EnemyStage > 100)
        {
            Shared.BattleMgr.EnemyStage -= 100;
            Shared.BattleMgr.enemyCount = 0;
            Shared.SceneMgr.ChangeScene(scene);
        }
        else if (NeedLv <= Shared.StatMgr.Lv)
        {
            Shared.BattleMgr.EnemyStage += 100;
            Shared.SceneMgr.ChangeScene(scene);
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
