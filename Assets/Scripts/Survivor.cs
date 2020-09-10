using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Survivor : MonoBehaviour
{
    [SerializeField]
    private int _maxFoodHunger;
    [SerializeField]
    private int _foodHunger;
    [SerializeField]
    private int _maxWaterHunger;
    [SerializeField]
    private int _WaterHunger;
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _health;
    [SerializeField]
    private string _rang;

    [SerializeField]
    private float _speed = 3.0f;
    private float gravity = 9.81f;

    private Pathfinding _gameManager;
    private List<Node> _path;
    private int counter = 0;
    Node temp = new Node();

    void Start()
    {
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.tag.Contains("GameManager"))
            {
                _gameManager = obj.GetComponent<Pathfinding>();
            }
        }
        _path = _gameManager.FindPath();
    }
    void Update()
    {
        if (counter == 0)
            temp = _path[counter++];
        if (transform.position.x == temp.vPosition.x && transform.position.z == temp.vPosition.z)
            if (counter < _path.Count)
                temp = _path[counter++];
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(temp.vPosition.x, 1.5f, temp.vPosition.z), Time.deltaTime * _speed);
    }
}