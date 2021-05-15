public interface IStateMachine
{
    IRobotState CurrentRobotState { get; }
    void ChangeCurrentState(IRobotState robotState);
}
