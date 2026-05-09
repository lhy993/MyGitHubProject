using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
public class Enemy_Boss_Goblin : Enemy_Boss
{
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
}
