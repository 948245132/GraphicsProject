using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("�ƶ����")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;

    [Space(10)]
    [Header("�������")]
    public float attackDistance;
    public float attackCoolDown;
    //�����ҵķ�Χ
    public float playerBattleDir;
    //׷����ҵľ���
    public float playerFollowDir;
    [HideInInspector] public float lastTimeAttack;


    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake() {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update() {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, playerBattleDir, whatIsPlayer);

    protected override void OnDrawGizmos() {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
}
