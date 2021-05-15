using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabingWaterState : IRobotState
{
    private RobotResourses _robotResourses;
    private Transform _fireTarget;
    private IStateMachine _stateMachine;
    public IStateMachine StateMachine => _stateMachine;

    public void Process(RobotResourses robotResourses)
    {
        throw new System.NotImplementedException();
    }
    
    private void GrabWater()
    {

    }

    private int ChangeRobotEnergy()
    {
        return 0;
    }

    private bool IsEnoughEnergy()
    {
        return true;
    }

    private void ChangeStateToFindEnergy()
    {

    }

    private void ChangeStateToFindFire()
    {

    }

    private void ChangeStateToPathfinding()
    {

    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
