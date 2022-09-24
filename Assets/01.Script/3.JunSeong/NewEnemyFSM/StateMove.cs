using UnityEngine;
using UnityEngine.AI;

public class StateMove : State<EnemyFSM>
{
    private CharacterController characterController;
    private NavMeshAgent nav;
    private Transform target;

    public override void OnAwake()
    {
        characterController = stateMachineClass.GetComponent<CharacterController>();
        nav = stateMachineClass.GetComponent<NavMeshAgent>();
    }

    public override void OnStart()
    {
        Debug.Log("Start Move");
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
        Debug.Log("End Move");
        nav.ResetPath();
    }

    public override void OnHitEvent()
    {
        
    }

    private void UnhitProcess()
    {
        Transform target = stateMachineClass.SearchPlayer();

        if(target)
        {
            Move<StateAttack>(target.position);
        }
        else
        {
            Move<StateIdle>(stateMachineClass.originPosition);
        }
    }

    private void HitProcess()
    {
        Move<StateAttack>(stateMachineClass.target.position);
    }

    private void Move<T>(Vector3 position) where T : State<EnemyFSM>
    {
        nav.SetDestination(position);

        if(nav.remainingDistance > nav.stoppingDistance)
        {
            characterController.Move(nav.velocity.normalized * stateMachineClass.speed * Time.deltaTime);
        }
        else
        {
            if(stateMachineClass.GetFlagAttack)
            {
                Debug.Log(1);
                stateMachine.ChangeState<T>();
            }
        }
    }
}
