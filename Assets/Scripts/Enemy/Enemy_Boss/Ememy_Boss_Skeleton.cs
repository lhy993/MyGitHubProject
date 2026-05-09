using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ememy_Boss_Skeleton : Enemy_Boss
{
    public override void EnemyStat()
    {
        Enemy_Lv = 30;
        Enemy_Max_Hp = 100 + (Enemy_Lv * 30);
        Enemy_Dmg = 1 + (Enemy_Lv * 1.5f);
        Enemy_Def = 10;

        Enemy_Hp = Enemy_Max_Hp;

        Right = true;

        chaseRange = 12f;
        attackRange = 8f;
        Shared.BattleMgr.enemyCount = 1;
    }
}
