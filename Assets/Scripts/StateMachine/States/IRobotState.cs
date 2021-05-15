public interface IRobotState
{
    IStateMachine StateMachine { get; }
    void Process(RobotResourses robotResourses);
}
