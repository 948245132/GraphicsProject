using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState {
    private Transform player;
    private Enemy_Skeleton enemy;
    private int moveDir;

    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName) {
        this.enemy = _enemy;
    }

    public override void Enter() {
        base.Enter();

        player = GameObject.Find("Player").transform;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        //如果找到玩家
        if (enemy.IsPlayerDetected()) {
            stateTimer = enemy.battleTime;
            //在攻击范围之内，停下来攻击
            if(enemy.IsPlayerDetected().distance < enemy.attackDistance) {
                if(CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position,enemy.transform.position) > enemy.playerFollowDir)
                stateMachine.ChangeState(enemy.idleState);
        }



        //如果玩家在右边则反转，反之依然
        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir , rb.velocity.y);
    }

    private bool CanAttack() {
        if (Time.time >= enemy.lastTimeAttack + enemy.attackCoolDown) {
            enemy.lastTimeAttack = Time.time;
            return true;
        }
        return false;
    }
}
