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
    [SerializeField] private float dashCoolDown;
    private float dashUsageTimer;
    public float dashDir { get; private set; }

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
    }

    protected override void Start() {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update() {
        base.Update();
        stateMachine.currentState.Update();

        CheckDashInput();

        StartCoroutine("BusyFor", 0.1f);
    }

    public IEnumerator BusyFor(float _seconds) {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    } 

    private void CheckDashInput() {
        if (IsWallDetected())
            return;

        dashUsageTimer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0) {

            dashUsageTimer = dashCoolDown;

            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0) {
                dashDir = facingDir;
            }

            stateMachine.ChangeState(dashState);
        }
    }

    public void AnimatorTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
