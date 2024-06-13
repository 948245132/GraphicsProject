using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger() {
        player.AnimatorTrigger();
    }

    /// <summary>
    /// ���ҹ���������ײ��
    /// </summary>
    private void AttackTrigger() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        
        //�ҵ���Ҫ�ܻ�����ײ��
        foreach(var hit in colliders) {
            if (hit.GetComponent<Enemy>() != null)
                hit.GetComponent<Enemy>().Damage();
        }
    }
}
