using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public partial class Character : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;

    public float attackRange = 1.5f;
    public LayerMask enemyLayer;
    public Vector2 attackOffset = new Vector2(1.0f, 0.5f);
    public bool isFacingRight = true;

    public bool block;
    public GameObject Revive;
    public Item reviveItem;
    public float moveDir = 0f;
    public bool Death = false;
    // 시작 시 초기화
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        Shared.StatMgr.Stat();
    }

    void Update()
    {
        if (!Death)
        {
            // 공격 콤보 타이머 증가
            m_timeSinceAttack += Time.deltaTime;
            //캐릭터가 지면에 착지했는지 확인
            if (!m_grounded && m_groundSensor.State())
            {
                m_grounded = true;
                m_animator.SetBool("Grounded", m_grounded);
            }

            //캐릭터가 낙하 시작했는지 확인
            if (m_grounded && !m_groundSensor.State())
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }

            if (moveDir != 0)
            {
                // 이동 방향에 따라 스프라이트 뒤집기
                if (moveDir > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    m_facingDirection = 1;
                }

                else if (moveDir < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    m_facingDirection = -1;
                }

                // 이동
                if (block == false)
                {
                    m_body2d.velocity = new Vector2(moveDir * m_speed, m_body2d.velocity.y);
                }
            }
            m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);
            //애니메이터 Y축 속도 설정
            m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

            // -- 애니메이션 처리 --
            //벽 슬라이드 여부 확인
            m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
            m_animator.SetBool("WallSlide", m_isWallSliding);

            //달리기 처리
            if (Mathf.Abs(moveDir) > Mathf.Epsilon)
            {
                //타이머 초기화
                m_delayToIdle = 0.05f;
                m_animator.SetInteger("AnimState", 1);
            }

            //대기 처리
            else
            {
                // Idle로의 깜빡임 전환 방지
                m_delayToIdle -= Time.deltaTime;
                if (m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
            }
        }
    }
    public void MoveLeftDown()
    {
        moveDir = -1;
    }
    public void MoveRightDown()
    {
        moveDir = 1;
    }
    public void MoveUp()
    {
        moveDir = 0;
        m_body2d.velocity = new Vector2(0, m_body2d.velocity.y);
    }
    public void BlockDown()
    {
        block = true;
        m_animator.SetTrigger("Block");
        m_animator.SetBool("IdleBlock", true);
    }
    public void BlockUp()
    {
        block = false;
        m_animator.SetBool("IdleBlock", false);
    }

    public void jumpBtn()
    {
        if (m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
    }
    public void AttackBtn()
    {
        if (m_timeSinceAttack > 0.5f && block == false)
        {
            m_currentAttack++;

            // 3번째 공격 후 1번 공격으로 루프
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // 공격 간격이 너무 길면 콤보 초기화
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            //  공격 애니메이션 호출
            m_animator.SetTrigger("Attack" + m_currentAttack);
            block = false;

            // 타이머 초기화
            m_timeSinceAttack = 0.0f;
        }
    }
    public void Skill()
    {
        if (Shared.StatMgr.Mp >= 50)
        {
            Shared.StatMgr.Mp -= 50;
            m_animator.SetTrigger("Skill");
            block = false;
        }
    }
    public void Attack()
    {
        Vector2 dir = Vector2.right * m_facingDirection;

        Vector2 attackPos =
            (Vector2)transform.position +
            dir * 1.5f +
            Vector2.up * 2f;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPos,
            attackRange,
            enemyLayer
        );

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(Shared.StatMgr.Dmg);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 dir = Vector2.right * m_facingDirection;

        Vector2 attackPos =
            (Vector2)transform.position +
            dir * 1.5f +
            Vector2.up * 2f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos, attackRange);
    }

    public void TakeDamage(float Damage)
    {

        if (!Death)
        {
            if (block == true)
            {
                Shared.StatMgr.Hp -= Damage * 0.3f;
                block = false;
            }
            else
            {
                Shared.StatMgr.Hp -= Damage * (1 - (Shared.StatMgr.Def / 100));
                //피해 처리
            }
            if (Shared.StatMgr.Hp > 0)
            {
                m_animator.SetTrigger("Hurt");
            }
            //죽음 처리
            else
            {
                if (Shared.BattleMgr.EnemyStage > 100)
                {
                    Shared.BattleMgr.life -= 1;
                }
                if (Shared.BattleMgr.EnemyStage > 100 && Shared.BattleMgr.life <= 0)
                {
                    if (Inventory.instance.HasItem(reviveItem, 1))
                    {
                        Revive.SetActive(true);
                        Time.timeScale = 0f;
                    }
                    else
                    {
                        Shared.SceneMgr.ChangeScene(SCENE.Battle);
                    }
                }
                Death = true;
                m_animator.SetBool("noBlood", m_noBlood);
                m_animator.SetTrigger("Death");
                StartCoroutine(Respawn());
            }
        }
    }
    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        Death = false;
        Shared.StatMgr.Hp = Shared.StatMgr.Max_Hp;
        transform.position = new Vector3(-15f, -3f, 0f);
        m_animator.SetTrigger("Hurt");
    }

    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
}

