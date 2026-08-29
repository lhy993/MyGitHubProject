    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dummy : MonoBehaviour
{
    [SerializeField]
    protected Animator ANIMATOR;

    public float Enemy_Hp = 100;
    string CurAni = "";

    public void Start()
    {
        SetAnimation("Idle");
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("더미 공격받음");
        ANIMATOR.SetTrigger("Hit");
        Shared.BattleMgr.ComboDmg += damage;
        Shared.BattleMgr.ComboHit += 1;
        Shared.BattleMgr.ComboTime = 5;
        if (Shared.BattleMgr.TutorialStage == 4)
        {
            Shared.BattleMgr.TutorialStage = 5;
        }
    }

    public void SetAnimation(string _Ani)
    {
        if (CurAni == _Ani)
            return;

        if (!string.IsNullOrEmpty(CurAni))
            ANIMATOR.SetBool(CurAni, false);

        ANIMATOR.SetBool(_Ani, true);

        CurAni = _Ani;
    }
}
