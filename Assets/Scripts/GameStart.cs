using UnityEngine;


public class GameStart : MonoBehaviour
{
    public SaveManager saveManager;



    void Start()
    {
        SaveData data =
            saveManager.LoadGame();



        if (data != null)
        {
            // 저장 데이터 있음
            LoadPlayer(data);
        }
        else
        {
            // 첫 실행
            CreateNewGame();
        }
    }



    void CreateNewGame()
    {
        SaveData data =
        new SaveData();


     


        data.level = 1;
        data.exp = 0;


        data.hp = 100;
        data.mp = 50;


        data.inventory =
        new System.Collections.Generic.List<ItemData>();


        data.clearStage = new bool[4];


        data.stat =
        new StatData()
        {
            dmgStat = 1,
            mpStat = 1,
            defStat = 1,
            hpStat = 1,
            statPoint = 0
        };


        saveManager.SaveGame(data);


        LoadPlayer(data);
    }



    void LoadPlayer(SaveData data)
    {
        // 실제 플레이어 데이터 적용
        // Player.level = data.level;
        // Inventory.Load(data.inventory);
    }
}