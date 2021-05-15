using UnityEngine;

public class Node
{
    public int Cost;
    public int AreaCost;

    public int GridX;//X Position in the Node Array
    public int GridY;//Y Position in the Node Array

    public bool Wall;//Tells the program if this node is being obstructed.
    public Vector3 Position;//The world position of the node.

    public Node ParentNode;//For the AStar algoritm, will store what node it previously came from so it cn trace the shortest path.

    public int IgCost;//The cost of moving to the next square.
    public int IhCost;//The distance to the goal from this node.

    public int FCost { get { return IgCost + IhCost + Cost + AreaCost; } }//Quick get function to add G cost and H Cost, and since we'll never need to edit FCost, we dont need a set function.

    public Node(bool wall, Vector3 position, int gridX, int gridY)//Constructor
    {
        Wall = wall;                    //Tells the program if this node is being obstructed.
        Position = position;            //The world position of the node.
        GridX = gridX;                  //X Position in the Node Array
        GridY = gridY;                  //Y Position in the Node Array
    }
    public Node() { }
}