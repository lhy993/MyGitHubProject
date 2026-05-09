using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    public override void EnemyStat()
    {
        min = 6;//최소 레벨
        max = min + 5;//레벨 최대 격차
        Enemy_Lv = rand.Next(min, max);
        Enemy_Max_Hp = 100 + (Enemy_Lv * 30);
        Enemy_Dmg = 1 + (Enemy_Lv * 2);
        Enemy_Def = 0;

        Enemy_Hp = Enemy_Max_Hp;

        Right = true;

        chaseRange = 5f;
        attackRange = 3f;

        Shared.BattleMgr.enemyCount = 2;
    }
}
