using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundState {
    public SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy) {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);
        
        //如果检测到墙壁但没有检测到地面，反转并进入idle状态
        if(enemy.IsWallDetected() || !enemy.IsGroundDetected()) {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
