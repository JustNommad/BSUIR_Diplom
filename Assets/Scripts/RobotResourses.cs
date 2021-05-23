
using UnityEngine;

public class RobotResourses
{
    public int Water { get; set; }
    public int Energy { get; set; }
    public int Speed { get; set; }
    public int MaxWater { get; set; }
    public int MaxEnergy { get; set; }
    public GameObject RobotObject { get; set; }
    public int RobotMode { get; set; }
    public IRobotState LastRobotState {get; set;}
    public IStateMachine StateMachine { get; set; }
}
