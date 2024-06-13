using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;

    protected Rigidbody2D rb;

    protected bool triggerCalled;
    protected float stateTimer;
    private string animBoolName;

    public EnemyState(Enemy _enemyBase,EnemyStateMachine _stateMachine ,string _animBoolName) {
        this.animBoolName = _animBoolName;
        this.stateMachine = _stateMachine;
        this.enemyBase = _enemyBase;
    }

    public virtual void Enter() {
        rb = enemyBase.rb;

        triggerCalled = false;
        enemyBase.anim.SetBool(animBoolName, true);
    }
    public virtual void Exit() {
        enemyBase.anim.SetBool(animBoolName, false);
    }
    public virtual void Update() {
        stateTimer -= Time.deltaTime;
    }

    public virtual void AnimationFinishTrigger() {
        triggerCalled = true;
    }
}
