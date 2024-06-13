public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }
    
    public void Initialize(EnemyState _enemyState) {

        currentState = _enemyState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState _newState) {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
