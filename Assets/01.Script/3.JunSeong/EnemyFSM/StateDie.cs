using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDie : State<EnemyFSM>
{
    public override void OnAwake()
    {
        
    }

    public override void OnEnd()
    {
        
    }

    public override void OnStart()
    {
        stateMachineClass.enemy.Die();
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }
}
