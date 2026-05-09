using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Trunt : Enemy
{

    public GameObject bulletPrefab;
    public Transform firePoint;
    public override void EnemyStat()
    {
        min = 11;//최소 레벨
        max = min + 5;//레벨 최대 격차
        Enemy_Lv = rand.Next(min, max);
        Enemy_Max_Hp = 100 + (Enemy_Lv * 30);
        Enemy_Dmg = 1 + (Enemy_Lv * 2);
        Enemy_Def = 0;

        Enemy_Hp = Enemy_Max_Hp;

        Right = false;

        chaseRange = 17f;
        attackRange = 15f;
        Shared.BattleMgr.enemyCount = 2;
    }

    public override void AttackDmg()
    {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            Vector2 dir = player.position - firePoint.position;

            bullet.GetComponent<Bullet>().Init(player, Enemy_Dmg);
            SetAnimation("Idle");
    }
}
