using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState { get; private set; }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize(PlayerState _startState) {
        currentState = _startState;
        currentState.Enter();
    }

    /// <summary>
    /// 改变状态
    /// </summary>
    public void ChangeState(PlayerState _newState) {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
