using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_Trunk : Enemy_Boss
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public override void EnemyStat()
    {
        Enemy_Lv = 45;
        Enemy_Max_Hp = 100 + (Enemy_Lv * 25);
        Enemy_Dmg = 3 + (Enemy_Lv * 5);
        Enemy_Def = 10;

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
