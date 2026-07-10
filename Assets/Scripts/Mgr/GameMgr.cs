using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    private void Awake()
    {
        if (Shared.GameMgr == null)
        {
            Shared.GameMgr = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartBtn()
    {
        SaveData data = Shared.SaveMgr.LoadGame();

        if (data != null)
        {
            Shared.SaveMgr.Load(data);
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
        Shared.StatMgr.Need = 0;
        Shared.StatMgr.Exp = 0;

        Shared.StatMgr.Hp = 100;
        Shared.StatMgr.Mp = 100;

        Shared.BattleMgr.Clear = new bool[4];

        Shared.StatMgr.Dmg_Stat = 0;
        Shared.StatMgr.Mp_Stat = 0;
        Shared.StatMgr.Def_Stat = 0;
        Shared.StatMgr.Hp_Stat = 0;
        Shared.StatMgr.Stat_point = 0;

        Shared.InventoryMgr.slots.Clear();
        Shared.SaveMgr.Save();

        Debug.Log("새로운 게임");
    }

}
