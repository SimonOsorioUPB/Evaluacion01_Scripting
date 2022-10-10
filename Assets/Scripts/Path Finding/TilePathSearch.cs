using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePathSearch : MonoBehaviour
{
    public static TilePathSearch Instance;
    
    [Header("General")]
    [SerializeField] private Tile _startingPoint;
    [SerializeField] private Tile _endingPoint;
    [SerializeField] private Color _startingPointColor;
    [SerializeField] private Color _endingPointColor;
    [SerializeField] private Color _pathColor;
    [SerializeField] private Color _exploredNodeColor;

    private Dictionary<Vector2Int, Tile> _block = new Dictionary<Vector2Int, Tile>();                           // For storing all the nodes with Node.cs
    private readonly Vector2Int[] _directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };    // Directions to search in BFS
    private Queue<Tile> _queue = new Queue<Tile>();         // Queue for enqueueing nodes and traversing through them
    private Tile _searchingPoint;                           // Current node we are searching
    private bool _isExploring = true;                       // If we are end then it is set to false

    public List<Tile> _path = new List<Tile>();

    public List<Tile> Path(Tile startingPoint, Tile endingPoint)
    {
        _startingPoint = startingPoint;
        _endingPoint = endingPoint;
        _path = null;
        _path = new List<Tile>();
        LoadAllBlocks(); BFS(startingPoint, endingPoint); CreatePath(startingPoint, endingPoint);
        return _path;
    }

    private void Awake(){ Instance = this; }

    public void LoadAllBlocks()
    {
        Tile[] nodes = FindObjectsOfType<Tile>();
        _block = null;
        _block = new Dictionary<Vector2Int, Tile>();

        foreach (Tile node in nodes) {
            Vector2Int gridPos = node.GetPos();
            // For checking if 2 nodes are in same position; i.e overlapping nodes
            node.isExplored = false;
            node.isExploredFrom = null;
            if (_block.ContainsKey(gridPos))
            {
                Debug.LogWarning("2 Nodes present in same position. i.e nodes overlapped.");
            }
            else
            {
                if (node.Walkable)
                {
                    _block.Add(gridPos, node);        // Add the position of each node as key and the Node as the value
                }
            }
        }
    }


    // BFS; For finding the shortest path
    private void BFS(Tile startingPoint, Tile endingPoint)
    {
        _queue = null;
        _queue = new Queue<Tile>();
        _queue.Enqueue(startingPoint);
        _searchingPoint = null;
        while (_queue.Count > 0 && _isExploring) {
            _searchingPoint = _queue.Dequeue();
            OnReachingEnd(endingPoint);
            ExploreNeighbourNodes();
        }
    }


    // To check if we've reached the Ending point
    private void OnReachingEnd(Tile endingPoint)
    {
        if (_searchingPoint == endingPoint) {
            _isExploring = false;
        }
        else
        {
            _isExploring = true;
        }
    }


    // Searching the neighbouring nodes
    private void ExploreNeighbourNodes()
    {
        if (!_isExploring) { return; }

        foreach (Vector2Int direction in _directions) {
            Vector2Int neighbourPos = _searchingPoint.GetPos() + direction;

            if (_block.ContainsKey(neighbourPos))               // If the explore neighbour is present in the dictionary _block, which contians all the blocks with Node.cs attached
            {
                Tile node = _block[neighbourPos];

                if (!node.isExplored)
                {
                    _queue.Enqueue(node);                       // Enqueueing the node at this position
                    node.isExplored = true;
                    node.GetComponentInChildren<SpriteRenderer>().material.color = _exploredNodeColor;
                    node.isExploredFrom = _searchingPoint;      // Set how we reached the neighbouring node i.e the previous node; for getting the path
                }
            }
        }
    }

    // Creating path using the isExploredFrom var of each node to get the previous node from where we got to this node
    public void CreatePath(Tile startingPoint, Tile endingPoint)
    {
        SetPath(endingPoint);
        Tile previousNode = endingPoint.isExploredFrom;

        while (previousNode != startingPoint) {
            SetPath(previousNode);
            previousNode = previousNode.isExploredFrom;
        }

        SetPath(startingPoint);
        _path.Reverse();
        SetPathColor();
        
    }

    // For adding nodes to the path
    private void SetPath(Tile node) {
        _path.Add(node);
    }

    // Setting color to nodes
    private void SetPathColor()
    {
        foreach (Tile node in _path) {
            node.GetComponentInChildren<SpriteRenderer>().material.color = _pathColor;
        }
        SetColor();
    }

    // Setting color to start and end position
    private void SetColor()
    {
        _startingPoint.GetComponentInChildren<SpriteRenderer>().material.color = _startingPointColor;
        _endingPoint.GetComponentInChildren<SpriteRenderer>().material.color = _endingPointColor;
    }
}
