using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();
    private void AnimationTrigger() {
        enemy.AnimationFinishTrigger();
    }

    /// <summary>
    /// ���ҹ���������ײ��
    /// </summary>
    private void AttackTrigger() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        //�ҵ���Ҫ�ܻ�����ײ��
        foreach (var hit in colliders) {
            if (hit.GetComponent<Player>() != null)
                hit.GetComponent<Player>().Damage();
        }
    }

    protected void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    protected void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
