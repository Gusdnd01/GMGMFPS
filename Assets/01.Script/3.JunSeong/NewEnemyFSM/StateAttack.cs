using UnityEngine;

public class StateAttack : State<EnemyFSM>
{
    private Transform characterTransform;
    private float rotateSpeed = 3f;
    private float angle = 5;

    public override void OnAwake()
    {
        characterTransform = stateMachineClass.GetComponent<Transform>();
    }

    public override void OnStart()
    {
        Debug.Log("Start Attack");
    }

    public override void OnUpdate(float deltaTime)
    {
        if(!stateMachineClass.isHit)
        {
            UnhitProcess();
        }
        else
        {
            HitProcess();
        }
    }

    public override void OnEnd()
    {
        Debug.Log("End Attack");
    }

    public override void OnHitEvent()
    {
        
    }

    private void UnhitProcess()
    {
        Transform target = stateMachineClass.SearchPlayer();

        if(target)
        {
            if(stateMachineClass.GetFlagAttack)
            {
                Vector3 dir = target.position - characterTransform.position;
                Quaternion _dir = Quaternion.LookRotation(dir);
                characterTransform.rotation = Quaternion.Lerp(characterTransform.rotation, _dir, rotateSpeed * Time.deltaTime);

                if(CheckAngle(target))
                {
                    Debug.Log("Attack");
                }
            }
            else
            {
                stateMachine.ChangeState<StateIdle>();
            }
        }
        else 
        {
            stateMachine.ChangeState<StateIdle>();
        }
    }

    private void HitProcess()
    {
        if(stateMachineClass.GetFlagAttack)
        {
            Vector3 dir = stateMachineClass.target.position - characterTransform.position;
            Quaternion _dir = Quaternion.LookRotation(dir);
            characterTransform.rotation = Quaternion.Lerp(characterTransform.rotation, _dir, rotateSpeed * Time.deltaTime);

            if(CheckAngle(stateMachineClass.target))
            {
                Debug.Log("Attack");
            }
        }
        else
        {
            stateMachine.ChangeState<StateMove>();
        }
    }

    private bool CheckAngle(Transform target)
    {
        Vector3 dir = (target.position - characterTransform.position).normalized;

        return Vector3.Dot(Vector3.forward, dir) <= Mathf.Cos((angle * 0.5f) * Mathf.Deg2Rad);
    }
}
