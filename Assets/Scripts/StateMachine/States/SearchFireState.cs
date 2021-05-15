using UnityEngine;

public class SearchFireState : IRobotState
{
    private IStateMachine _stateMachine;
    public IStateMachine StateMachine => _stateMachine;

    public void Process(RobotResourses robotResourses)
    {
        throw new System.NotImplementedException();
    }

    private GameObject[] SearchFireObjects()
    {
        return null;
    }

    private GameObject FindClosest(GameObject[] fireObjects)
    {
        return null;
    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
