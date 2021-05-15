using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingState : IRobotState
{
    private RobotResourses _robotResourses;
    private IRobotState _lastRobotState;
    private IStateMachine _stateMachine;
    public IStateMachine StateMachine => _stateMachine;

    public void Process(RobotResourses robotResourses)
    {
        throw new System.NotImplementedException();
    }

    private void CHargingProcess()
    {

    }

    private bool CheckEnergyStatus()
    {
        return true;
    }

    private void CheckLastRobotState()
    {

    }

    private void ChangeCurrentStateToWaterSearching()
    {

    }

    private void ChangeCUrrentStateToFireSearching()
    {

    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
