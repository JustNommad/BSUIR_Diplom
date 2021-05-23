using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateMachine : MonoBehaviour, IStateMachine
{
    [SerializeField] private RobotConfig _robotConfig;
    [SerializeField] private TMP_Text _waterStatus;
    [SerializeField] private TMP_Text _energyStatus;

    private IRobotState _currentRobotState;
    private RobotResourses _robotResourses;
    private long _lastTime;
    public IRobotState CurrentRobotState => _currentRobotState;

    void Awake()
    {
        _robotResourses = new RobotResourses
        {
            Water = _robotConfig.Water,
            Energy = _robotConfig.Energy,
            Speed = _robotConfig.Speed,
            MaxEnergy = _robotConfig.MaxEnergy,
            MaxWater = _robotConfig.MaxWater,
            RobotObject = gameObject,
            RobotMode = 0,
            StateMachine = this
        };

        _currentRobotState = new SearchWaterState();
    }

    void Update()
    {
        var currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;

        if (_currentRobotState as MovementState == null)
        {
            if (currentTime < _lastTime + 1)
                return;
        }

        CurrentRobotState.Process(_robotResourses);

        _waterStatus.text = $"Water: {_robotResourses.Water}";
        _energyStatus.text = $"Energy: {_robotResourses.Energy}";

        _lastTime = currentTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentRobotState.TriggerEvent(other.gameObject);
    }

    public void ChangeCurrentState(IRobotState robotState)
    {
        _currentRobotState = robotState;
    }
}
