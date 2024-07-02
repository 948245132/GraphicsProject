using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        //玩家投掷 玩家没有sword
        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
            stateMachine.ChangeState(player.aimSword);

        //玩家反击
        if (Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(player.counterAttack);

        //玩家攻击
        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);

        //玩家不处于地面，进入空状态
        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        //玩家在地面进行跳跃
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);
    }

    private bool HasNoSword() {
        if (!player.sword)
            return true;


        player.sword.GetComponent<SwordSkillController>().ReturnSword();
        return false;
    }
}
