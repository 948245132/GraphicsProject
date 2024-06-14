using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected Player player;

    [Header("冷却")]
    [SerializeField] protected float coolDown;
    protected float coolDownTimer;

    protected virtual void Start() {
        player = PlayerManager.instance.player;
    }

    protected virtual void Update() {
        coolDownTimer -= Time.deltaTime;
    }

    /// <summary>
    /// 是否可以使用技能
    /// </summary>
    /// <returns>是或否</returns>
    public virtual bool CanUseSkill() {
        if(coolDownTimer < 0) {
            UseSkill();
            coolDownTimer = coolDown;
            return true;
        }
        return false;
    }

    public virtual void UseSkill() {
        //TODO: 使用技能
    }
}
