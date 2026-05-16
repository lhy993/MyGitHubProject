using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.XR;
public class Enemy_Boss : Enemy
{
    public Text BossHPText;

    public Slider BossHpSlider;

    public GameObject PORTAL;

    public override void EnemyAwake()
    {
        BossHPText = GameObject.FindWithTag("BossHpText").GetComponent<Text>();
        BossHpSlider = GameObject.FindWithTag("BossBar").GetComponent<Slider>();
        PORTAL = GameObject.FindWithTag("Portal");
    }
    public override void EnemyStart()
    {
        PORTAL.SetActive(false);
    }
    public override void EnemyStat()
    {
        Enemy_Lv = 15;
        Enemy_Max_Hp = 100 + (Enemy_Lv * 50);
        Enemy_Dmg = 3 + (Enemy_Lv * 2); 
        Enemy_Def = 10;

        Enemy_Hp = Enemy_Max_Hp;

        Right = true;

        chaseRange = 12f;
        attackRange = 8f;
        Shared.BattleMgr.enemyCount = 1;
    }
    public override void StatText()
    {   
    }
    public override void EnemyUpdate()
    {
        if ( BossHPText != null && BossHpSlider != null)
        {
            BossHpSlider.maxValue = Enemy_Max_Hp;
            BossHpSlider.value = Enemy_Hp;

            BossHPText.text = ("Hp " + Enemy_Hp.ToString() + " / " + Enemy_Max_Hp.ToString());
        }
        else
        {
            Debug.Log("null");
        }
    }
    public override void Drop()
    {
        Random = rand.Next(1, 6);
        Inventory.instance.AddItem(EnemyDrop, Random);
        Shared.StatMgr.Exp += Enemy_Lv * Enemy_Lv * 10;
    }
    public override void Die()
    {
        BossHpSlider.gameObject.SetActive(false);
        PORTAL.SetActive(true);
        Shared.BattleMgr.enemyCount--;
        Drop();
        Destroy(gameObject);
        Shared.BattleMgr.Clear[Shared.BattleMgr.EnemyStage - 100] = true;
        switch (Shared.BattleMgr.EnemyStage)
        {
            case 101://°íºí¸°
                {
                    Shared.BattleMgr.Clear[0] = true;
                    break;
                }
            case 102://½ºÄÌ·¹Åæ 
                {
                    Shared.BattleMgr.Clear[1] = true;
                    break;
                }
        }
    }
}
