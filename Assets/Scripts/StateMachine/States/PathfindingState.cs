using System.Collections.Generic;
using UnityEngine;

public class PathfindingState : IRobotState
{
    private IStateMachine _stateMachine;
    public IStateMachine StateMachine => _stateMachine;

    public void Process(RobotResourses robotResourses)
    {
        throw new System.NotImplementedException();
    }

    private List<Node> FindPathToTarget()
    {
        return null;
    }

    private int ChangeEnergy()
    {
        return 0;
    }

    private bool IsEnoughEnergy()
    {
        return true;
    }

    private void ChangeStateToFindeEnergy()
    {

    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
