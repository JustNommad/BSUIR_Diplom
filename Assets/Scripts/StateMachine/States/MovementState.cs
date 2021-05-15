using System.Collections.Generic;

public class MovementState : IRobotState
{
    private List<Node> _pathList;
    private IRobotState _targetState;
    private IStateMachine _stateMachine;
    public IStateMachine StateMachine => _stateMachine;

    public void Process(RobotResourses robotResourses)
    {
        throw new System.NotImplementedException();
    }
    
    private void MoveRobot()
    {

    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
