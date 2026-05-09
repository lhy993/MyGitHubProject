using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UI_Battle : MonoBehaviour
{

    public Text HP_STAT;
    public Text MP_STAT;
    public Text DMG_STAT;
    public Text DEF_STAT;
    
    public Text STAT_POINT;

    public Text GOLDTEXT;

    public GameObject MENU;
    public GameObject TP;

    public InputField STATinputField;

    public Text HPText;
    public Text EXPText;
    public Text MPText;

    public Text COMBO;

    public GameObject[] HUNTSTAGE = new GameObject[3];

    public int savedValue;

    bool Tp = false;
    bool Menu = false;

    public Slider hpSlider;

    public Slider expSlider;

    public Slider mpSlider;

    public GameObject INVENTORY;
    public GameObject ITEMDETA;

    public bool inventory_On;

    public GameObject inventory_return;

    public GameObject Portal_Btn;
    public GameObject[] HEART = new GameObject[3];
    public Item reviveItem;
    public GameObject Revive;
    public GameObject BossHp;
    public bool BossStage;

    public GameObject inventory_on;
    public GameObject tp_on;
    public GameObject stat_on;
    public GameObject GoldUi;
    public GameObject Hurdle;

    public GameObject SkeletonPrefab;
    public GameObject GoblinPrefab;
    public GameObject TrunkPrefab;
    public GameObject SkeletonBoss;
    public GameObject GoblinBoss;
    public GameObject TrunkBoss;
    void Start()
    {
        inventory_On = true;
        inventory();
        Reset();
    }
    public void Reset()
    {
        if (Shared.BattleMgr.EnemyStage < 100)
        {
            BossStage = false;
        }
        else
        {
            BossStage = true;
        }
        Revive.SetActive(false);
        HEART[0].SetActive(BossStage);
        HEART[1].SetActive(BossStage);
        HEART[2].SetActive(BossStage);
        BossHp.SetActive(BossStage);
        inventory_on.SetActive(!BossStage);
        tp_on.SetActive(!BossStage);
        stat_on.SetActive(!BossStage);
        GoldUi.SetActive(!BossStage);
        Hurdle.SetActive(!BossStage);

        switch (Shared.BattleMgr.EnemyStage)
        {
            case 1:
                {
                    Instantiate(GoblinPrefab, new Vector3(5, -2, 0), Quaternion.identity);
                    Instantiate(GoblinPrefab, new Vector3(-7, 4, 0), Quaternion.identity);
                    break;
                }
            case 2:
                {
                    Instantiate(SkeletonPrefab, new Vector3(5, -2, 0), Quaternion.identity);
                    Instantiate(SkeletonPrefab, new Vector3(-7, 4, 0), Quaternion.identity);
                    break;
                }
            case 3:
                {
                    Instantiate(TrunkPrefab, new Vector3(3, -2, 0), Quaternion.identity);
                    Instantiate(TrunkPrefab, new Vector3(5, -2, 0), Quaternion.identity);
                    break;
                }
            case 101:
                {
                    Instantiate(GoblinBoss, new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                }
            case 102:
                {
                    Instantiate(SkeletonBoss, new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                }
            case 103:
                {
                    Instantiate(TrunkBoss, new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                }
        }
        Shared.BattleMgr.enemyCount = 2;
    }
    void Update()
    {
        Bar();

        Stat();

        if (Shared.BattleMgr.EnemyStage < 100)
        {
            Gold();
        }

        if (Shared.BattleMgr.EnemyStage > 100)
        {
            BossUiUpdate();
        }

        if (Shared.BattleMgr.ComboHit > 0)
        {
            COMBO.text = (Shared.BattleMgr.ComboHit + "Hit\n" +Shared.BattleMgr.ComboDmg + "Dmg");
        }
        else
        {
            COMBO.text = "";
        }
            Portal_Btn.SetActive(Shared.BattleMgr.playerInRange);

        if(Shared.BattleMgr.PortalReset  == true)
        {
            Shared.BattleMgr.PortalReset = false;
            Reset();    
        }
    }
    public virtual void inventory()
    {
        if (Shared.BattleMgr.EnemyStage < 100)
        {
            inventory_On = !inventory_On;
            INVENTORY.SetActive(inventory_On);
            inventory_return.SetActive(inventory_On);
            ITEMDETA.SetActive(false);
            Tp = false;
            TP.SetActive(false);
            Menu = false;
            MENU.SetActive(false);
        }
    }
    public virtual void BossUiUpdate()
    {
        if (HEART.Length > Shared.BattleMgr.life)
        {
            if (Shared.BattleMgr.life <= -1)
            {
                return;
            }
            HEART[Shared.BattleMgr.life].SetActive(false);
        }
    }
        public virtual void Gold()
        {   
            GOLDTEXT.text = (Shared.UserMgr.gold.ToString());
        }
    public virtual void Bar()
    {
        hpSlider.maxValue = Shared.StatMgr.Max_Hp;
        hpSlider.value = Shared.StatMgr.Hp;

        expSlider.maxValue = Shared.StatMgr.Need;
        expSlider.value = Shared.StatMgr.Exp;

        mpSlider.maxValue = Shared.StatMgr.Max_Mp;
        mpSlider.value = Shared.StatMgr.Mp;

        HPText.text = ("Hp " + Mathf.FloorToInt(Shared.StatMgr.Hp) + " / " + Shared.StatMgr.Max_Hp.ToString());
        EXPText.text = (Shared.StatMgr.Lv.ToString() + "LV    " + "Exp " + Shared.StatMgr.Exp.ToString() + " / " + Shared.StatMgr.Need.ToString());
        MPText.text = ("Mp " + Shared.StatMgr.Mp.ToString() + " / " + Shared.StatMgr.Max_Mp.ToString());
    }
    public virtual void Stat()
    {
        HP_STAT.text = ("Hp : " + Shared.StatMgr.Hp_Stat);
        MP_STAT.text = ("Mp : " + Shared.StatMgr.Mp_Stat);
        DMG_STAT.text = ("Dmg : " + Shared.StatMgr.Dmg_Stat);
        DEF_STAT.text = ("Def : " + Shared.StatMgr.Def_Stat);
        
        if (Input.GetKeyDown("e"))
        {
            MenuBtn();
        }

        STAT_POINT.text = ("Stat point : " + Shared.StatMgr.Stat_point);

        if (int.TryParse(STATinputField.text, out int result))
        {
            savedValue = result;
        }
        else
        {
            savedValue = 1;// 숫자가 아니면 기본값
            STATinputField.text = "1";
        }
    }
    public void LobbyBtn()
    {
            Shared.SceneMgr.ChangeScene(SCENE.Lobby);   
    }

    public void MenuBtn()
    {
        Menu = !Menu;

        MENU.SetActive(Menu);
        Tp = false;
        TP.SetActive(false);
        inventory_On = false;
        INVENTORY.SetActive(false);
        inventory_return.SetActive(false);
    }
    public void TpBtn()
    {
        Lock();
        Tp = !Tp;

        TP.SetActive(Tp);
        Menu = false;
        MENU.SetActive(false);
        inventory_On = false;
        INVENTORY.SetActive(false);
        inventory_return.SetActive(false);
    }
    public void Dmg_Stat_Btn()
    {
        if (Shared.StatMgr.Stat_point >= savedValue)
        {
            Shared.StatMgr.Stat_point -= savedValue;

            Shared.StatMgr.Dmg_Stat += savedValue;
        }
    }
    public void Lock()
    {
        for (int i = 0; i < (Shared.BattleMgr.FinalStage + 1); i++)
        {
            HUNTSTAGE[i].SetActive(Shared.BattleMgr.Clear[i]);
        }
    }
    public void TpGoblin()
    {
        Shared.BattleMgr.enemyCount = 0;
        Shared.BattleMgr.EnemyStage = 1;
        Shared.SceneMgr.ChangeScene(SCENE.Battle_Goblin);
    }
    public void TpSkeleton()
    {
        Shared.BattleMgr.enemyCount = 0;
        Shared.BattleMgr.EnemyStage = 2;
        Shared.SceneMgr.ChangeScene(SCENE.Battle_Skeleton);
    }
    public void TpTrunk()
    {
        Shared.BattleMgr.enemyCount = 0;
        Shared.BattleMgr.EnemyStage = 3;
        Shared.SceneMgr.ChangeScene(SCENE.Battle_Trunk);
    }
    public void Hp_Stat_Btn()
    {
        if (Shared.StatMgr.Stat_point >= savedValue)
        {
            Shared.StatMgr.Stat_point -= savedValue;

            Shared.StatMgr.Hp_Stat += savedValue;
        }
    }
    public void Mp_Stat_Btn()
    {
        if (Shared.StatMgr.Stat_point >= savedValue)
        {
            Shared.StatMgr.Stat_point -= savedValue;

            Shared.StatMgr.Mp_Stat += savedValue;
        }
    }
    public void Def_Stat_Btn()
    {
        if (Shared.StatMgr.Stat_point >= savedValue)
        {
            Shared.StatMgr.Stat_point -= savedValue;

            Shared.StatMgr.Def_Stat += savedValue;
        }
    }
    public void ReviveOn()
    {
        Time.timeScale = 1f;
        Revive.SetActive(false);
        HEART[0].SetActive(true);
        Inventory.instance.RemoveItemByItem(reviveItem, 1);
        Shared.BattleMgr.life++;
    }
    public void ReviveOff()
    {
        Time.timeScale = 1f;
        Revive.SetActive(false);
        Shared.SceneMgr.ChangeScene(SCENE.Lobby);
    }
}

