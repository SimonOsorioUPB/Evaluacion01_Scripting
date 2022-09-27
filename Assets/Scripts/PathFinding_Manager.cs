using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding_Manager : MonoBehaviour
{
    private static float[,] tilesmap = new float[10, 10];
    private static Grid grid = new Grid(tilesmap);

    private static Point _from = new Point(1, 1), _to = new Point(10, 10); 
    
    List<Point> path = Pathfinding.FindPath(grid, _from, _to);

    private void Update()
    {
        grid.UpdateGrid (tilesmap);
    }
}
