using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : Skill
{
    [Header("投掷技能相关")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;

    private Vector2 finalDir;

    [Space(10)]
    [Header("瞄准相关")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBeetwenDot;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParents;

    private GameObject[] dots;

    protected override void Start() {
        base.Start();

        GenereateDots();
    }

    protected override void Update() {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
        
        if (Input.GetKey(KeyCode.Mouse1)) {
            for (int i = 0; i < dots.Length; i++) {
                dots[i].transform.position = DotsPosition(i * spaceBeetwenDot);
            }
        }
    }

    public void CreatSword() {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position,transform.rotation);
        SwordSkillController newSwordScript = newSword.GetComponent<SwordSkillController>();

        newSwordScript.SetupSword(finalDir, swordGravity,player);

        player.AssignNewSword(newSword);

        DotsActive(false);
    }

    #region 扔剑相关

    /// <summary>
    /// 获取剑扔向的位置
    /// </summary>
    /// <returns></returns>
    public Vector2 AimDirection() {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool _isActive) {
        for (int i = 0; i < dots.Length; i++) {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenereateDots() {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++) {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParents);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t) {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + 0.5f * (Physics2D.gravity * swordGravity) * (t * t);

        return position;
    }

    #endregion
}
