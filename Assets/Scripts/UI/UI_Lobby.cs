using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class UI_Lobby : MonoBehaviour
{
    public Text Tip;

    public GameObject LOBBY;
    public GameObject HUNT;
    public GameObject UPGRADE;
    public GameObject[] HUNTSTAGE = new GameObject[2];
    public GameObject SHOP;
    public GameObject SELL;
    public GameObject BUY;
    public GameObject GOLD;

    public Text GOLDTEXT;

    // Start is called before the first frame update
    void Start()
    {
        Active(LOBBY);
        Lock();
    }
    void Update()
    {
        GOLDTEXT.text = (Shared.UserMgr.gold.ToString());
    }
    public void Lock()
    {
        for (int i = 0; i < (Shared.BattleMgr.FinalStage + 1); i++)
        {
            HUNTSTAGE[i].SetActive(Shared.BattleMgr.Clear[i]);
        }
    }
    public void SafeBuy()
    {
        if(Shared.UserMgr.gold >= 200)
        {
            Shared.UserMgr.gold -= 200;
            Shared.UserMgr.Safe += 1;
        }
    }
    public void GiblinBtn()
    {
        Shared.BattleMgr.enemyCount = 0;
        Shared.BattleMgr.EnemyStage = 1;
        Shared.SceneMgr.ChangeScene(SCENE.Battle_Goblin);
    }
    public void Skeleton_Btn()
    {
        Shared.BattleMgr.enemyCount = 0;
        Shared.BattleMgr.EnemyStage = 2;
        Shared.SceneMgr.ChangeScene(SCENE.Battle_Skeleton);
    }

    public void Trunk_Btn()
    {
        Shared.BattleMgr.enemyCount = 0;
        Shared.BattleMgr.EnemyStage = 3;
        Shared.SceneMgr.ChangeScene(SCENE.Battle_Trunk);
    }
    public void ReturnBtn()
    {
        Active(LOBBY);
    }

    public void HuntBtn()
    {
        Active(HUNT);
    }

    public void UpgradeUiBtn()
    {
        Active(UPGRADE);
    }

    public void ShopBtn()
    {
        Active(SHOP);
    }
    public void SellBtn()
    {
        Active(SELL);
    }
    public void BuyBtn()
    {
        Active(BUY);
    }

    public void Active(GameObject e)
    {
        LOBBY.SetActive(false);
        HUNT.SetActive(false);
        UPGRADE.SetActive(false);
        SHOP.SetActive(false);
        SELL.SetActive(false);
        BUY.SetActive(false);

        e.SetActive(true);

        if (e == UPGRADE || e == SELL || e == BUY)
        {
            GOLD.SetActive(true);
        }
        else
        {
            GOLD.SetActive(false);
            
        }
    }


}
