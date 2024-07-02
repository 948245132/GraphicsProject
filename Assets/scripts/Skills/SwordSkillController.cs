using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    [SerializeField] private float returnSpeed = 12;
    [Space(10)]
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D cd;
    [SerializeField] private Player player;

    private bool canRotate = true;
    private bool isReturning;

    public void SetupSword(Vector2 _dir , float _gravityScale,Player _player) {
        player = _player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        anim.SetBool("Rotation", true);
    }

    private void Update() {
        if(canRotate)
            transform.right = rb.velocity;

        //返回sword
        if (isReturning) {
            transform.position = Vector2.MoveTowards(transform.position, 
                player.transform.position, 
                returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.ClearTheSword();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        anim.SetBool("Rotation", false);

        //让sword看向目标
        //transform.LookAt(collision.transform,Vector3.up);

        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }

    /// <summary>
    /// 返回Sword
    /// </summary>
    public void ReturnSword() {
        rb.isKinematic = false;
        transform.parent = null;

        isReturning = true;
    }

}
