using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;
    [Header("眩晕相关")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Space(10)]
    [Header("移动相关")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;

    [Space(10)]
    [Header("攻击相关")]
    public float attackDistance;
    public float attackCoolDown;
    //检测玩家的范围
    public float playerBattleDir;
    //追击玩家的距离
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

    #region 判断生效

    /// <summary>
    /// 判断是否可以进行反击
    /// </summary>
    public virtual void OpenCounterAttackWindow() {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow() {
        canBeStunned = false;
        counterImage.SetActive(false);
    } 

    public virtual bool CanBeStunned() {
        if (canBeStunned) {
            CloseCounterAttackWindow();
            return true ;
        }
        return false;
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, playerBattleDir, whatIsPlayer);
    #endregion

    #region 测试
    protected override void OnDrawGizmos() {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
    #endregion
}
