using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ҹ���
/// ����ģʽ
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;

    private void Awake() {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
}
