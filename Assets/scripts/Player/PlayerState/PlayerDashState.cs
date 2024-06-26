using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState {
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) {
    }

    public override void Enter() {
        base.Enter();

        //冲刺时创建一个克隆体
        player.skill.clone.CreatClone(player.transform);

        stateTimer = player.dashDiration;
    }

    public override void Exit() {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update() {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);
        //if(player.IsGroundDetected() && !player.IsWallDetected())

           

        player.SetVelocity(player.dashSpeed * player.dashDir,0);
        
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }
}
