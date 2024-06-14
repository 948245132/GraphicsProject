using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [Header("��ȴ")]
    [SerializeField] protected float coolDown;
    protected float coolDownTimer;

    protected virtual void Update() {
        coolDownTimer -= Time.deltaTime;
    }

    /// <summary>
    /// �Ƿ����ʹ�ü���
    /// </summary>
    /// <returns>�ǻ��</returns>
    public virtual bool CanUseSkill() {
        if(coolDownTimer < 0) {
            UseSkill();
            coolDownTimer = coolDown;
            return true;
        }
        return false;
    }

    public virtual void UseSkill() {
        //TODO: ʹ�ü���
    }
}
