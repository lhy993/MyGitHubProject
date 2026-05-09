using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public class UiLogin : MonoBehaviour
{
    public InputField NICKNAME;
    // Start is called before the first frame update
    void Start()
    {

    }


    public void Guest_check()
    {
        if (!string.IsNullOrWhiteSpace(NICKNAME.text))
        {
            Shared.UserMgr.UserName = NICKNAME.text;
            Shared.BattleMgr.enemyCount = 0;
            Shared.BattleMgr.EnemyStage = 1;
            Shared.SceneMgr.ChangeScene(SCENE.Battle_Goblin);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}

