using UnityEngine;
using System.IO;


public class SaveMgr : MonoBehaviour
{
    string path;

    void Awake()
    {
        path =
        Application.persistentDataPath
        + "/SaveData.json";
        if (Shared.SaveMgr == null)
        {
            Shared.SaveMgr = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log(path);
    }


    public void SaveGame(SaveData data)
    {
        string json =
        JsonUtility.ToJson(data, true);


        File.WriteAllText(path, json);


        Debug.Log("저장 완료");
    }



    public SaveData LoadGame()
    {
        if (File.Exists(path))
        {
            string json =
            File.ReadAllText(path);


            SaveData data =
            JsonUtility.FromJson<SaveData>(json);


            Debug.Log("불러오기 성공");


            return data;
        }


        return null;
    }

    public void Save()
    {
        SaveData data = new SaveData();

        data.level = Shared.StatMgr.Lv;
        data.need = Shared.StatMgr.Need;
        data.exp = Shared.StatMgr.Exp;

        data.gold = Shared.UserMgr.gold;

        data.hp = Shared.StatMgr.Hp;

        data.mp = Shared.StatMgr.Mp;



        data.inventory = Shared.InventoryMgr.GetSaveData();



        data.clearStage = Shared.BattleMgr.Clear;



        data.stat = new StatData();

        data.stat.dmgStat = Shared.StatMgr.Dmg_Stat;

        data.stat.mpStat = Shared.StatMgr.Mp_Stat;

        data.stat.defStat = Shared.StatMgr.Def_Stat;

        data.stat.hpStat = Shared.StatMgr.Hp_Stat;

        data.stat.statPoint = Shared.StatMgr.Stat_point;



        SaveGame(data);
        Debug.Log("저장됨");
    }

    public void Load(SaveData data)
    {   
        Shared.StatMgr.Lv = data.level;
        Shared.StatMgr.Need = data.need;
        Shared.StatMgr.Exp = data.exp;

        Shared.UserMgr.gold = data.gold;

        Shared.StatMgr.Hp = data.hp;
        Shared.StatMgr.Mp = data.mp;

        Shared.BattleMgr.Clear = data.clearStage;

        Shared.InventoryMgr.LoadData(data.inventory);

        Shared.StatMgr.Dmg_Stat = data.stat.dmgStat;
        Shared.StatMgr.Mp_Stat = data.stat.mpStat;
        Shared.StatMgr.Def_Stat = data.stat.defStat;
        Shared.StatMgr.Hp_Stat = data.stat.hpStat;
        Shared.StatMgr.Stat_point = data.stat.statPoint;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
    }
}