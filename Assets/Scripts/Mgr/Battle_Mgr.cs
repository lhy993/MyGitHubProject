using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class BattleMgr : MonoBehaviour
{
    private void Awake()
    {
        if (Shared.BattleMgr == null)
        {
            Shared.BattleMgr = this;

            DontDestroyOnLoad(this);    
        }
    }
    public bool recovery;
    public int EnemyStage;
    public bool isRespawning = false;
    public int life;

    public int enemyCount;

    public float ComboDmg;
    public float ComboHit;
    public float ComboTime;

    public bool playerInRange = false;

    public GameObject SkeletonPrefab;
    public GameObject GoblinPrefab;
    public GameObject TrunkPrefab;

    public int FinalStage = 3;

    public bool PortalReset;

    public bool[] Clear = new bool[4];
    void Start()
    {
        enemyCount = 1;
        Clear[0] = true;
        recovery = true;
    }
    IEnumerator EnemyRespawn()
    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        yield return new WaitForSeconds(2f);
        switch (EnemyStage)
        {
            case 1://°íºí¸°
                {
                    Instantiate(GoblinPrefab, new Vector3(5, -2, 0), Quaternion.identity);
                    Instantiate(GoblinPrefab, new Vector3(-7, 4, 0), Quaternion.identity);
                    break;
                }
            case 2://½ºÄÌ·¹Åæ 
                {
                    Instantiate(SkeletonPrefab, new Vector3(5, -2, 0), Quaternion.identity);
                    Instantiate(SkeletonPrefab, new Vector3(-7, 4, 0), Quaternion.identity);    
                    break;
                }
            case 3://Æ®··Å©
                {
                    Instantiate(TrunkPrefab, new Vector3(3, -2, 0), Quaternion.identity);
                    Instantiate(TrunkPrefab, new Vector3(5, -2, 0), Quaternion.identity);
                    break;
                }
        }
        enemyCount = 2;
        isRespawning = false;
    }
    void Update()
    {
        if (enemyCount <= 0 && !isRespawning)
        {
            isRespawning = true;
            StartCoroutine(EnemyRespawn());
        }
        if (recovery)
        {
            StartCoroutine(RecoveryUpdate());
        }
        if (ComboTime > 0)
        {
            ComboTime -= Time.deltaTime;
        }      
        else
        {
            ComboHit = 0;
            ComboDmg = 0;
        }
    } 
    IEnumerator RecoveryUpdate()
    {
        recovery = false;
        yield return new WaitForSeconds(5f);
        Shared.StatMgr.Hp = Recovery(Shared.StatMgr.Hp, Shared.StatMgr.Max_Hp);
        Shared.StatMgr.Mp = Recovery(Shared.StatMgr.Mp, Shared.StatMgr.Max_Mp);
        recovery = true;
    }
    float Recovery(float f,float maxf)
    {
        if (f < maxf)
        {
            f += (maxf * 3) / 100;
        }
        if (f > maxf)
        {
            f = maxf;
        }

        return f;
    }
}