using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float colorLoosingSpeed;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = 0.8f;
     private Transform closestEnemy;

    private float cloneTimer;

    private void Update() {
        cloneTimer -= Time.deltaTime;

        if(cloneTimer < 0) {
            sr.color = new Color(1,1,1,sr.color.a - (Time.deltaTime * colorLoosingSpeed));

            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }

    public void SetupClone(Transform _newTransform,float _cloneDuration, bool _canAttack) {
        if (_canAttack) {
            anim.SetInteger("AttackNumber", Random.Range(1, 3));
        }

        transform.position = _newTransform.position;
        cloneTimer = _cloneDuration;

        FaceClosestTarge();
    }

    private void FaceClosestTarge() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closetDistance = Mathf.Infinity;

        foreach(var hit in colliders) {
            if(hit.GetComponent<Enemy>() != null) {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if(distanceToEnemy < closetDistance) {
                    closetDistance = distanceToEnemy;

                    closestEnemy = hit.transform;
                }
            }
        }

        if(closestEnemy != null) {
            if (transform.position.x > closestEnemy.position.x)
                transform.Rotate(0, 180, 0);
        }
    }

    #region ��д�����¼�

    private void AnimationTrigger() {
        cloneTimer = -0.1f;
    }

    /// <summary>
    /// ���ҹ���������ײ��
    /// </summary>
    private void AttackTrigger() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        //�ҵ���Ҫ�ܻ�����ײ��
        foreach (var hit in colliders) {
            if (hit.GetComponent<Enemy>() != null)
                hit.GetComponent<Enemy>().Damage();
        }
    }

    #endregion
}
