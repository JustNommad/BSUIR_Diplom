using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingState : IRobotState
{
    private const int ChargingPerSecond = 20;

    private RobotResourses _robotResourses;
    private IStateMachine _stateMachine;
    public IStateMachine StateMachine => _stateMachine;

    public void Process(RobotResourses robotResourses)
    {
        if(_robotResourses == null)
        {
            _robotResourses = robotResourses;
            _stateMachine = _robotResourses.StateMachine;
        }

        if (_stateMachine == null)
            return;

        ChargingProcess();

        if(CheckEnergyStatus())
        {
            CheckLastRobotState();
        }
    }

    public void TriggerEvent(GameObject objectTrigger)
    {
    }

    private void ChargingProcess()
    {
        _robotResourses.Energy += ChargingPerSecond;
    }

    private bool CheckEnergyStatus()
    {
        if(_robotResourses.Energy >= _robotResourses.MaxEnergy)
        {
            _robotResourses.Energy = _robotResourses.MaxEnergy;

            return true;
        }

        return false;
    }

    private void CheckLastRobotState()
    {
        if (_robotResourses.LastRobotState == null)
            return;

        if(_robotResourses.LastRobotState is GrabingWaterState)
        {
            ChangeCurrentStateToWaterSearching();
        }
        else if (_robotResourses.LastRobotState is ExtinguishingFireState)
        {
            ChangeCurrentStateToFireSearching();
        }
    }

    private void ChangeCurrentStateToWaterSearching()
    {
        ChangeCurrentState(new SearchWaterState());
    }

    private void ChangeCurrentStateToFireSearching()
    {
        ChangeCurrentState(new SearchFireState());
    }

    private void ChangeCurrentState(IRobotState robotState)
    {
        _stateMachine.ChangeCurrentState(robotState);
    }
}
