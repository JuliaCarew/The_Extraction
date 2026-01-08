
public interface IState
{
    GameState Id { get; }
    void Enter();
    void Exit();
    void Update();
}
