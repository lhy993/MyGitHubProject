using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Mgr : MonoBehaviour
{
    public SaveManager saveManager;
    public Inventory inventory;
    public void Save()
    {
        SaveData data = new SaveData();

        data.level = Shared.StatMgr.Lv;

        data.exp = Shared.StatMgr.Exp;


        data.hp = Shared.StatMgr.Hp;

        data.mp = Shared.StatMgr.Mp;



        data.inventory = inventory.GetSaveData();



        data.clearStage = Shared.BattleMgr.Clear;



        data.stat = new StatData();

        data.stat.dmgStat = Shared.StatMgr.Dmg_Stat;

        data.stat.mpStat = Shared.StatMgr.Mp_Stat;

        data.stat.defStat = Shared.StatMgr.Def_Stat;

        data.stat.hpStat = Shared.StatMgr.Hp_Stat;

        data.stat.statPoint = Shared.StatMgr.Stat_point;



        saveManager.SaveGame(data);
    }
}
