using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public LayerMask targetLayerMask;
    public Transform target;    
    public EnemyData data;
    public Vector3 originPosition;
    public bool isHit = false;
    public float speed;

    public bool GetFlagAttack
    {
        get
        {
            if(target == null)
            {
                return false;
            }

            float distacne = Vector3.Distance(transform.position, target.position);
            
            return(distacne <= data.attackDistance);
        }
    }

    protected StateMachine<EnemyFSM> fsmManager;

    private void Start() 
    {
        fsmManager = new StateMachine<EnemyFSM>(this, new StateIdle());
        fsmManager.AddStateList(new StateMove());
        fsmManager.AddStateList(new StateAttack());
        fsmManager.AddStateList(new StateStun());

        originPosition = transform.position;
    }

    private void Update() 
    {
        fsmManager.Update(Time.deltaTime);
    }

    public void OnHitEvent(Transform transform)
    {
        fsmManager.OnHitEvent();

        if(!isHit)
        {
            target = transform;
            isHit = true;
            speed = data.runSpeed;
        }

        fsmManager.ChangeState<StateStun>();
    }

    public Transform SearchPlayer()
    {
        target = null;

        Collider[] findTargets = Physics.OverlapSphere(transform.position, data.eyeSight, targetLayerMask);
        if(findTargets.Length > 0)
        {
            target = findTargets[0].transform;
        }

        return target;
    }
}
