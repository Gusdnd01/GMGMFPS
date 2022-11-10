using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class EnemyFSM : MonoBehaviour
{
    private StateMachine<EnemyFSM> fsmManager;

    public float findRadius;
    public float attackDistance;
    public float speed;

    public float idleTime = 1f;
    public float moveTime = 3f;

    public Vector3 originPos = Vector3.zero;

    public bool FlagAttack
    {
        get
        {
            if(target == null)
            {
                return false;
            }

            return Vector3.Distance(transform.position, target.position) < attackDistance;
        }
    }
    public Transform target;
    public Animator animator;
    public EnemyBase enemy;

    public int moveSpeedHash = Animator.StringToHash("MoveSpeed");

    private void Awake() 
    {
        originPos = transform.position;

        fsmManager = new StateMachine<EnemyFSM>(this, new StateIdle());
        fsmManager.AddState(new StateMove());
        fsmManager.AddState(new StateAttack());

        animator = GetComponent<Animator>();       
    }

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        speed = enemy.MoveSpeed;
        attackDistance = enemy.minAttackDistance;
    }

    private void Update()
    {
        fsmManager.Update(Time.deltaTime);
    }

    public void OnHit()
    {
        fsmManager.NowState.OnHit();
    }

    public Transform SearchPlayer()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, findRadius, 1 << 7);

        if(col.Length > 0)
        {
            target = col[0].transform;
            return target;
        }

        return null;
    }

    public void SetAttackPattern(EnemyBase _enemy)
    {
        this.enemy = _enemy;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
     
        Gizmos.DrawWireSphere(transform.position, 2);    
    }
    #endif
}
