using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实例的基础模板
/// </summary>
public class Entity : MonoBehaviour
{

    #region 组件相关
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    #endregion

    [Header("受击相关")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    [Space(10)]
    [Header("碰撞相关")]
    public Transform attackCheck;
    public float attackCheckRadius;
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
        fx = GetComponent<EntityFX>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update(){

    }

    public virtual void Damage() {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKonckback");
       // Debug.Log(gameObject.name + "was Damage");
    }

    protected virtual IEnumerator HitKonckback() {
        isKnocked = true;

        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);

        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
        rb.velocity = new Vector2(0, 0);
    }

    #region 速度相关

    public void SetZeroVelocity() {
        //如果角色正处于受击状态则无法控制
        if (isKnocked)
            return;

        rb.velocity = new Vector2(0, 0);
    
    }

        /// <summary>
        /// 设置速度
        /// </summary>
        /// <param name="_xVelocity">x方向的速度</param>
        /// <param name="_yVelocity">y方向的速度</param>
        public void SetVelocity(float _xVelocity, float _yVelocity) {
        //如果角色正处于受击状态则无法控制
        if (isKnocked)
            return;

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FilpController(_xVelocity);
    }

    #endregion

    #region 控制角色的反转
    /// <summary>
    /// 反转函数
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

    #region 检测地面和墙壁
    /// <summary>
    /// 检测是否在地面
    /// </summary>
    /// <returns>是或否</returns>
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    /// <summary>
    /// 检测墙壁
    /// </summary>
    /// <returns>是或否</returns>
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    #endregion

    #region 测试代码
    protected virtual void OnDrawGizmos() {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));

        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

}
