using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public int hp;

    public float walkSpeed;
    public float runSpeed;
    public float jumpSpeed;
    public float maxSpeed;

    public float activeDistance;

    public float attackDistance;
    public int attackPower;
    public float attackRadius;
    public float attackDelay;
    public float attackAngle;

    public float stunTime;
}
