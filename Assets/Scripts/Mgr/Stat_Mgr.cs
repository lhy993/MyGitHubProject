
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMgr : MonoBehaviour
{
    private void Awake()
    {
        if (Shared.StatMgr == null)
        {
            Shared.StatMgr = this;

            DontDestroyOnLoad(this);
        }
    }
    public float Max_Mp = 0;
    public float Max_Hp = 0;
    public float Def = 0;
    public float Dmg = 0;
    public float attackRange = 3f;

    public float Mp = 0;
    public float Hp = 0;

    public int Hp_Stat = 0;
    public int Dmg_Stat = 0;
    public int Def_Stat = 0;
    public int Mp_Stat = 0;

    public float Need = 0;
    public float Exp = 0;
    public int Lv = 0;
    public int Stat_point = 0;

public float Sword_Dmg;

    private void Start()
    {   
        Stat();
        Hp = Max_Hp;
        Mp = Max_Mp;
    }
    public void Stat()
    {
        Max_Hp = 50 + (10 * Hp_Stat);

        Dmg = Sword_Dmg + (Sword_Dmg * ((float)Dmg_Stat/100));

        Def = Def_Stat / 10;

        Max_Mp = 100 + Mp_Stat * 10;
    }


    void LvUp()
    {
        if (Exp >= Need)
        {
            Exp -= Need;
            Lv += 1;
            Stat_point += 3;
        }
        Need = Lv * 30 * Lv;
    }
    private void Update()
    {
        Stat();
        LvUp();
    }
}
