using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Mgr : MonoBehaviour
{
    public SaveManager saveManager;
    public Inventory inventory;

    void Start()
    {
        SaveData data = saveManager.LoadGame();

        if (data != null)
        {
            Load(data);
        }
        else
        {
            CreateNewGame();
        }

        Shared.SceneMgr.ChangeScene(SCENE.Battle);
    }

    void CreateNewGame()
    {
        Shared.StatMgr.Lv = 0;
        Shared.StatMgr.Exp = 0;

        Shared.StatMgr.Hp = 100;
        Shared.StatMgr.Mp = 100;

        Shared.BattleMgr.Clear = new bool[4];

        Shared.StatMgr.Dmg_Stat = 0;
        Shared.StatMgr.Mp_Stat = 0;
        Shared.StatMgr.Def_Stat = 0;
        Shared.StatMgr.Hp_Stat = 0;
        Shared.StatMgr.Stat_point = 0;

        inventory.slots.Clear();
        Save();
    }
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

    void Load(SaveData data)
    {
        Shared.StatMgr.Lv = data.level;
        Shared.StatMgr.Exp = data.exp;

        Shared.StatMgr.Hp = data.hp;
        Shared.StatMgr.Mp = data.mp;

        Shared.BattleMgr.Clear = data.clearStage;

        inventory.LoadData(data.inventory);

        Shared.StatMgr.Dmg_Stat = data.stat.dmgStat;
        Shared.StatMgr.Mp_Stat = data.stat.mpStat;
        Shared.StatMgr.Def_Stat = data.stat.defStat;
        Shared.StatMgr.Hp_Stat = data.stat.hpStat;
        Shared.StatMgr.Stat_point = data.stat.statPoint;
    }


}
