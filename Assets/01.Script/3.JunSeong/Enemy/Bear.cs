using UnityEngine;
using System.Collections;

public class Bear : EnemyBase
{
    private enum AttackPos
    {
        center,
        front
    }
    private AttackPos attackPos;
    private int damage;

    [Header("앞발 휘두르기")]
    public int wieldDamage = 20; 
    public float wieldRange = 2f;
    public float wieldAttackDis = 4.75f;
    public float wieldTime;

    [Header("앞발 내려치기")]
    public int downSlapDamage = 30;
    public float downSlapRange = 8f;
    public float downSlapAttackDis = 10f;
    public float downSlapTime;

    [Header("크게 내려치기")]
    public int bigDownSlapDamage = 50;
    public float bigDownSlapRange = 10f;
    public float bigDownSlapAttackDis = 15f;
    public float bigDownSlapTime;

    [Header("돌진")]
    public int rushDamage = 40;
    public float rushSpeed = 10f;
    public float rushTime;
    public LayerMask wallLayer;
    private bool rushHit;
    private bool isRush;

    [Header("포효")]
    public float ShoutTime;

    private Animator animator;

    private int moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private int wieldHash = Animator.StringToHash("Wield");
    private int downSlapHash = Animator.StringToHash("DownSlap");
    private int bigDownSlapHash = Animator.StringToHash("BigDownSlap");
    private int rushHash = Animator.StringToHash("Rush");

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        GetComponent<EnemyFSM>().SetAttackPattern(this);
    }

    public override void Attacking(Transform target)
    {
        Debug.Log(Distance);

        if(Distance <= wieldAttackDis)
        {
            Wield();
        }
        else if(Distance <= downSlapAttackDis)
        {
            DownSlap();
        }
        else if(Distance <= bigDownSlapAttackDis)
        {
            BigDownSlap();
        }
        else
        {
            Rush();
        }
    } 

    private void Wield()
    {
        Debug.Log("Wield");

        animator.SetTrigger(wieldHash);

        attackPos = AttackPos.front;
        damage = wieldDamage;
    }

    private void DownSlap()
    {
        Debug.Log("DownSlap");

        animator.SetTrigger(downSlapHash);

        attackPos = AttackPos.center;
        damage = downSlapDamage;
    }

    private void BigDownSlap()
    {
        Debug.Log("BigDownSlap");

        animator.SetTrigger(bigDownSlapHash);

        attackPos = AttackPos.center;
        damage = bigDownSlapDamage;
    }

    private void Rush()
    {
        Debug.Log("Rush");

        animator.SetBool(rushHash, true);

        isRush = true;
        rushHit = false;

        damage = rushDamage;
        StartCoroutine(DoRush());
    }

    private void Shout()
    {
        Debug.Log("Shout");

        //
    }

    private IEnumerator DoRush()
    {
        float rushDeltaTime = 0f;

        while(rushDeltaTime <= rushTime)
        {
            yield return null;

            controller.Move(transform.forward * rushSpeed * Time.deltaTime);
            rushDeltaTime += Time.deltaTime;

            if(Vector3.Distance(transform.position, player.position) <= 2 && !rushHit)
            {
                rushHit = true;
                player.GetComponent<IDamage>().OnDamaged(damage);
            }
        }

        animator.SetBool(rushHash, false);
        isRush = false;
        EndAttack();

        Debug.Log("end rush");
    }

    public void CheckHit(float attackRadius)
    {
        Vector3 attackingPos = Vector3.zero;

        if(attackPos == AttackPos.center)
        {
            attackingPos = transform.position;
        }
        else
        {
            attackingPos = transform.position + transform.forward * 2;
        }

        Collider[] hit = Physics.OverlapSphere(attackingPos, attackRadius, playerLayer);

        if (hit.Length > 0)
        {
            hit[0].GetComponent<IDamage>().OnDamaged(damage);
            Debug.Log("attack");
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, 3.5f);
        Gizmos.DrawWireSphere(transform.position, 7);
        Gizmos.DrawWireSphere(transform.position, 10);
    }
#endif
}
