using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ʵ���Ļ���ģ��
/// </summary>
public class Entity : MonoBehaviour
{

    #region ������
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    [Space(10)]
    [Header("��ײ���")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;


    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    protected virtual void Awake() {

    }
    protected virtual void Start(){
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update(){

    }

    #region �ٶ����

    public void SetZeroVelocity() => rb.velocity = new Vector2(0, 0);

    /// <summary>
    /// �����ٶ�
    /// </summary>
    /// <param name="_xVelocity">x������ٶ�</param>
    /// <param name="_yVelocity">y������ٶ�</param>
    public void SetVelocity(float _xVelocity, float _yVelocity) {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FilpController(_xVelocity);
    }

    #endregion

    #region ���ƽ�ɫ�ķ�ת
    /// <summary>
    /// ��ת����
    /// </summary>
    public void Flip() {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FilpController(float _x) {
        if (_x > 0 && !facingRight) {
            Flip();
        }
        else if (_x < 0 && facingRight) {
            Flip();
        }
    }

    #endregion

    #region �������ǽ��
    /// <summary>
    /// ����Ƿ��ڵ���
    /// </summary>
    /// <returns>�ǻ��</returns>
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    /// <summary>
    /// ���ǽ��
    /// </summary>
    /// <returns>�ǻ��</returns>
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    #endregion

    #region ���Դ���
    protected virtual void OnDrawGizmos() {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
    }
    #endregion

}
