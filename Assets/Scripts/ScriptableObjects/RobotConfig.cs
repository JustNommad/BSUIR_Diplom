using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Robot Config")]
public class RobotConfig : ScriptableObject
{
    [SerializeField] private int _water;
    [SerializeField] private int _energy;
    [SerializeField] private int _speed;
    [SerializeField] private int _maxWater;
    [SerializeField] private int _maxEnergy;

    public int Water => _water;
    public int Energy => _energy;
    public int Speed => _speed;
    public int MaxWater => _maxWater;
    public int MaxEnergy => _maxEnergy;
}
