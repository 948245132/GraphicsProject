using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("反击相关")]
    public float counterAttackDuration = .2f;

    [Space(10)]
    [Header("攻击相关")]
    public Vector2[] attackMovement;

    public bool isBusy { get; private set; }

    [Space(10)]
    [Header("移动相关")]
    public float moveSpeed = 6f;
    public float jumpForce = 6f;
    
    [Space(10)]
    [Header("冲刺相关")]
    public float dashSpeed;
    public float dashDiration;
   
    public float dashDir { get; private set; }
    public SkillManager skill { get; private set; }
    public GameObject sword; //{ get;  private set; }

    #region 状态相关

    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }
    public PlayerAimSwordState aimSword { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }

    #endregion

    protected override void Awake() {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");

        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

        aimSword = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
    }

    protected override void Start() {
        base.Start();

        skill = SkillManager.instance;

        stateMachine.Initialize(idleState);
    }

    protected override void Update() {
        base.Update();
        stateMachine.currentState.Update();

        CheckDashInput();

        StartCoroutine("BusyFor", 0.1f);
    }

    #region sword相关

    /// <summary>
    /// 分配新sword
    /// </summary>
    public void AssignNewSword(GameObject _newSword) {
        sword = _newSword;
    }

    /// <summary>
    /// 清楚Sword
    /// </summary>
    public void ClearTheSword() {
        Destroy(sword);
    }

    #endregion

    public IEnumerator BusyFor(float _seconds) {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    } 

    private void CheckDashInput() {
        if (IsWallDetected())
            return;

       // dashUsageTimer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && skill.dash.CanUseSkill()) {

           // dashUsageTimer = dashCoolDown;

            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0) {
                dashDir = facingDir;
            }

            stateMachine.ChangeState(dashState);
        }
    }

    public void AnimatorTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
