using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour 
{
    [SerializeField] private Transform _startPosition;//Starting position to pathfind from
    [SerializeField] private Transform _targetPosition;//Starting position to pathfind to

    private PathArea _pathAreaReference;//For referencing the grid class

    private void Awake()//When the program starts
    {
        _pathAreaReference = GetComponent<PathArea>();//Get a reference to the game manager
    }

    public List<Node> FindPath()
    {
        Node StartNode = _pathAreaReference.NodeFromWorldPoint(_startPosition.position);//Gets the node closest to the starting position
        Node TargetNode = _pathAreaReference.NodeFromWorldPoint(_targetPosition.position);//Gets the node closest to the target position

        List<Node> OpenList = new List<Node>();//List of nodes for the open list
        HashSet<Node> ClosedList = new HashSet<Node>();//Hashset of nodes for the closed list

        OpenList.Add(StartNode);//Add the starting node to the open list to begin the program

        while(OpenList.Count > 0)//Whilst there is something in the open list
        {
            Node CurrentNode = OpenList[0];//Create a node and set it to the first item in the open list
            for(int i = 1; i < OpenList.Count; i++)//Loop through the open list starting from the second object
            {
                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].IhCost < CurrentNode.IhCost)//If the f cost of that object is less than or equal to the f cost of the current node
                {
                    CurrentNode = OpenList[i];//Set the current node to that object
                }
            }
            OpenList.Remove(CurrentNode);//Remove that from the open list
            ClosedList.Add(CurrentNode);//And add it to the closed list

            if (CurrentNode == TargetNode)//If the current node is the same as the target node
            {
                return GetFinalPath(StartNode, TargetNode);//Calculate the final path
            }

            foreach (Node NeighborNode in _pathAreaReference.GetNeighboringNodes(CurrentNode))//Loop through each neighbor of the current node
            {
                if (!NeighborNode.Wall || ClosedList.Contains(NeighborNode))//If the neighbor is a wall or has already been checked
                {
                    continue;//Skip it
                }
                int MoveCost = CurrentNode.IgCost + GetManhattenDistance(CurrentNode, NeighborNode);//Get the F cost of that neighbor

                if (MoveCost < NeighborNode.IgCost || !OpenList.Contains(NeighborNode))//If the f cost is greater than the g cost or it is not in the open list
                {
                    NeighborNode.IgCost = MoveCost;//Set the g cost to the f cost
                    NeighborNode.IhCost = GetManhattenDistance(NeighborNode, TargetNode);//Set the h cost
                    NeighborNode.ParentNode = CurrentNode;//Set the parent of the node for retracing steps

                    if(!OpenList.Contains(NeighborNode))//If the neighbor is not in the openlist
                    {
                        OpenList.Add(NeighborNode);//Add it to the list
                    }
                }
            }
        }
        return null;
    }

    private List<Node> GetFinalPath(Node startingNode, Node endNode)
    {
        List<Node> FinalPath = new List<Node>();//List to hold the path sequentially 
        Node CurrentNode = endNode;//Node to store the current node being checked

        while(CurrentNode != startingNode)//While loop to work through each node going through the parents to the beginning of the path
        {
            FinalPath.Add(CurrentNode);//Add that node to the final path
            CurrentNode = CurrentNode.ParentNode;//Move onto its parent node
        }

        FinalPath.Reverse();//Reverse the path to get the correct order

        _pathAreaReference.FinalPath = FinalPath;//Set the final path
        return FinalPath;
    }

    private int GetManhattenDistance(Node nodeA, Node nodeB)
    {
        int ix = Mathf.Abs(nodeA.GridX - nodeB.GridX);//x1-x2
        int iy = Mathf.Abs(nodeA.GridY - nodeB.GridY);//y1-y2

        return ix + iy;//Return the sum
    }
}