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
    public float speed = 5;

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
    //public Animator animator;
    public  Boss boss;

    private void Awake() 
    {
        originPos = transform.position;

        fsmManager = new StateMachine<EnemyFSM>(this, new StateIdle());
        fsmManager.AddState(new StateMove());
        fsmManager.AddState(new StateAttack());

        //animator = GetComponent<Animator>();
        boss = GetComponent<Bear>();
        Debug.Log(boss);
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

    public void CheckHIt()
    {
        Collider[] col = Physics.OverlapSphere(transform.position + transform.forward, 1.5f, 1 << 7);

        if(col.Length > 0)
        {
            Debug.Log("개같이 처맞");
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(originPos, findRadius);
        Gizmos.DrawWireSphere(transform.position, attackDistance);    
    }
    #endif
}
