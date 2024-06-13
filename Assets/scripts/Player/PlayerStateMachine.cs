using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState { get; private set; }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    public void Initialize(PlayerState _startState) {
        currentState = _startState;
        currentState.Enter();
    }

    /// <summary>
    /// �ı�״̬
    /// </summary>
    public void ChangeState(PlayerState _newState) {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
