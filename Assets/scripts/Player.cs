using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Player : Entity
{
    [Header("�ƶ����������Ϣ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("�������������Ϣ")]
    [SerializeField] private bool isAttacking;
    [SerializeField] private int comboCounter;

    //����������ʱ��
    [SerializeField] private float comboTime = .3f;
    //�����м�ʱ��
    private float comboTimeWindow;

    [Header("��̡��������Ϣ")]
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
    /// ��������
    /// </summary>
    public void AttackOver() {
        isAttacking = false;

        comboCounter++;

        if (comboCounter > 2) comboCounter = 0;
    }

    /// <summary>
    /// ��̿���
    /// </summary>
    private void DashControllers() {
        dashTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            dashTime = dashDuration;
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    private void CheckInput() {
        xInput = Input.GetAxisRaw("Horizontal");

        //�������
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            StartAttackEvent();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }

    /// <summary>
    /// ��ʼ����
    /// </summary>
    private void StartAttackEvent() {
        //�����ɫû���ŵأ�����������
        if (!isGrounded) return;

        if (comboTimeWindow < 0) comboCounter = 0;

        isAttacking = true;

        comboTimeWindow = comboTime;
    }

    /// <summary>
    /// �ƶ�
    /// </summary>
    private void Movement() {
        if(dashTime > 0) {
            //�����泯������
            rb.velocity = new Vector2(facingDir * dashSpeed , 0);
        }
        else {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
    }

    /// <summary>
    /// ����ڵ�����������Ծ
    /// </summary>
    private void Jump() {
        if(isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    /// <summary>
    /// ��������
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
    /// ��ת����
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