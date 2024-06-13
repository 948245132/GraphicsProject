using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Player : Entity
{
    [Header("移动——相关信息")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("攻击——相关信息")]
    [SerializeField] private bool isAttacking;
    [SerializeField] private int comboCounter;

    //连击间隔最短时间
    [SerializeField] private float comboTime = .3f;
    //连击中间时间
    private float comboTimeWindow;

    [Header("冲刺——相关信息")]
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    
    private float xInput;

    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        Movement();
        CheckInput();
        DashControllers();

        FlipController();
        AnimatorControllers();

        comboTimeWindow -= Time.deltaTime;
    }

    /// <summary>
    /// 攻击结束
    /// </summary>
    public void AttackOver() {
        isAttacking = false;

        comboCounter++;

        if (comboCounter > 2) comboCounter = 0;
    }

    /// <summary>
    /// 冲刺控制
    /// </summary>
    private void DashControllers() {
        dashTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            dashTime = dashDuration;
        }
    }

    /// <summary>
    /// 检查输出
    /// </summary>
    private void CheckInput() {
        xInput = Input.GetAxisRaw("Horizontal");

        //左键攻击
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            StartAttackEvent();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }

    /// <summary>
    /// 开始攻击
    /// </summary>
    private void StartAttackEvent() {
        //如果角色没有着地，不触发攻击
        if (!isGrounded) return;

        if (comboTimeWindow < 0) comboCounter = 0;

        isAttacking = true;

        comboTimeWindow = comboTime;
    }

    /// <summary>
    /// 移动
    /// </summary>
    private void Movement() {
        if(dashTime > 0) {
            //向着面朝方向冲锋
            rb.velocity = new Vector2(facingDir * dashSpeed , 0);
        }
        else {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
    }

    /// <summary>
    /// 如果在地面则允许跳跃
    /// </summary>
    private void Jump() {
        if(isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    /// <summary>
    /// 动画控制
    /// </summary>
    private void AnimatorControllers() {
        bool isMoving = rb.velocity.x != 0;

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0);

        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }

    /// <summary>
    /// 反转控制
    /// </summary>
    private void FlipController() {
        if(rb.velocity.x > 0 && !facingRight) {
            Flip();
        }
        else if(rb.velocity.x < 0 && facingRight) {
            Flip();
        }
    }
}
*/