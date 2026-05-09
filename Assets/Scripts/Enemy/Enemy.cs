using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected Animator ANIMATOR;

    string CurAni = "";
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float patrolDistance = 3f;
    public float chaseRange = 5f;
    public float attackRange = 3f;
    Vector3 patrolCenter;   // 순찰 기준점

    public float StopRange = 3f;
    public float attackCooldown = 3f;
    bool movingRight = true;

    public bool movingLeft = true;

    public bool Right = true;
    public Transform player;

    public GameObject PLAYER;

    private Vector3 startPos;

    private SpriteRenderer spriteRenderer;
    public System.Random rand = new System.Random();

    public TMP_Text Stat;

    public float Enemy_Hp = 0;
    public float Enemy_Def = 0;
    public float Enemy_Dmg = 0;
    public float Enemy_Max_Hp = 0;
    public int Enemy_Lv = 0;
    public float lastAttackTime;
    public int min;
    public int max;
    public int Random;
    public Item EnemyDrop;
    public GameObject EnemyPrefab;
    enum State
    {
        Patrol,
        Chase,
        Attack
    }

    State currentState;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer가 Enemy 오브젝트에 없습니다!");
        }
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
        if (PLAYER == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                PLAYER = p.gameObject;
        }
    }
    public virtual void EnemyAwake()
    {

    }
    void Start()
    {
        EnemyStat();
        patrolCenter = transform.position;
        currentState = State.Patrol;
    }
    public void Respawn()
    {

    }
    public virtual void EnemyStat()
    {
        min = 1;//최소 레벨
        max += 5;//레벨 최대 격차
        Enemy_Lv = rand.Next(min, max);
        Enemy_Max_Hp = 100 + (Enemy_Lv * 50);
        Enemy_Dmg = 3 + (Enemy_Lv * 2);
        Enemy_Def = 0;

        Enemy_Hp = Enemy_Max_Hp;

        Right = true;

        chaseRange = 5f;
        attackRange = 3f;
        Shared.BattleMgr.enemyCount = 2;
    }
    void Update()
    {
        StatText();
        EnemyUpdate();
        if (player == null) return;

        float distanceToPlayer =
            Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                if (distanceToPlayer <= chaseRange)
                {
                    currentState = State.Chase;
                }
                else
                {
                    SetAnimation("Run");
                    Patrol();
                }
                break;

            case State.Chase:
                if (distanceToPlayer > chaseRange)
                {
                    currentState = State.Patrol;
                    patrolCenter = transform.position;
                    movingRight = true;
                }
                else if (distanceToPlayer <= attackRange)
                {
                    currentState = State.Attack;
                }
                else
                {
                    SetAnimation("Run");
                    ChasePlayer();
                }
                break;

            case State.Attack:
                if (distanceToPlayer > attackRange)
                {
                    currentState = State.Chase;
                }
                else
                {
                    Attack();
                }
                break;
        }
    }
    public virtual void EnemyUpdate()
    {

    }
    public virtual void StatText()
    {
        Stat.text = (Enemy_Lv + "Lv " + Mathf.FloorToInt(Enemy_Hp) + "Hp");
    }
    public void Attack()
    {
        // 방향 바라보기
        Vector2 direction = (player.position - transform.position).normalized;
        Flip(-direction.x);

        // 쿨타임 체크
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;

        // 실제 공격 처리
        ANIMATOR.SetTrigger("Attack");
    }

    public virtual void AttackDmg()
    {
        PLAYER.GetComponent<Character>()?.TakeDamage(Enemy_Dmg);
        SetAnimation("Idle");
    }

    void Patrol()
    {
        float dir = movingRight ? 1 : -1;


        Flip(-dir);
        transform.Translate(Vector2.right * dir * patrolSpeed * Time.deltaTime);

        float distanceFromCenter =
            Vector2.Distance(transform.position, patrolCenter);

        if (distanceFromCenter >= patrolDistance)
        {
            movingRight = !movingRight;
        }
    }


    void ChasePlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        // 방향 바라보기
        Vector2 direction = (player.position - transform.position).normalized;
        Flip(-direction.x);

        // 너무 가까우면 멈춤
        if (distance <= StopRange)
            return;

        // 일정 거리 이상일 때만 이동
        transform.Translate(direction * chaseSpeed * Time.deltaTime);
    }
    public virtual void Flip(float dir)
    {
        if (dir > 0)
            spriteRenderer.flipX = Right; // 오른쪽
        else if (dir < 0)
            spriteRenderer.flipX = !Right;  // 왼쪽
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public virtual void Drop()
    {
        Random = rand.Next(Enemy_Lv, max);
        if (Random == max - 1)
        {
            Inventory.instance.AddItem(EnemyDrop, 1);
        }
        Shared.StatMgr.Exp += Enemy_Lv * Enemy_Lv * 10;
    }

    public virtual void TakeDamage(float damage)
    {
        ANIMATOR.SetTrigger("Hit");
        Enemy_Hp -= damage;
        Shared.BattleMgr.ComboDmg += damage;
        Shared.BattleMgr.ComboHit += 1;
        Shared.BattleMgr.ComboTime = 5;

        if (Enemy_Hp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Shared.BattleMgr.enemyCount--;
        Drop();
        Destroy(gameObject);
    }

    protected void SetAnimation(string _Ani)
    {
        if (CurAni == _Ani)
            return;

        if (!string.IsNullOrEmpty(CurAni))
            ANIMATOR.SetBool(CurAni, false);

        ANIMATOR.SetBool(_Ani, true);   

        CurAni = _Ani;
    }
}
