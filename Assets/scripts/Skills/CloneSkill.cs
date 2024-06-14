using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Space(10)]
    [Header("克隆相关")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [SerializeField] private bool canAttack;

    /// <summary>
    /// 创建克隆体
    /// </summary>
    public void CreatClone(Transform _cloneTransform) {
        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<CloneSkillController>().SetupClone(_cloneTransform,cloneDuration, canAttack);
    }

}
