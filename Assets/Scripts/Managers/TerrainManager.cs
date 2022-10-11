using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    
    public Dictionary<Vector2, int> TerrainType;

    private List<Vector2> waterTiles, mountainTiles;
    private int tileType = 1; // 0 = Mountain, 1 = Ground, 2 = Water

    private void Awake()
    {
        SetAllTiles();
        SetMountainTiles();
        //SetWaterTiles();
    }

    void SetAllTiles()
    {
        TerrainType = new Dictionary<Vector2, int>();
        for (int x = 0; x < _gridManager.Width + 1; x++)
        {
            for (int y = 0; y < _gridManager.Height + 1; y++)
            {
                TerrainType.Add(new Vector2(x, y), 1);
            }
        }
    }

    void SetMountainTiles()
    {
        mountainTiles = new List<Vector2>();
        
        mountainTiles.Add(new Vector2(8,0));
        mountainTiles.Add(new Vector2(9,0));
        mountainTiles.Add(new Vector2(10,0));
        mountainTiles.Add(new Vector2(0,1));
        mountainTiles.Add(new Vector2(8,1));
        mountainTiles.Add(new Vector2(9,1));
        mountainTiles.Add(new Vector2(15,1));
        mountainTiles.Add(new Vector2(0,2));
        mountainTiles.Add(new Vector2(1,2));
        mountainTiles.Add(new Vector2(2,2));
        mountainTiles.Add(new Vector2(6,2));
        mountainTiles.Add(new Vector2(13,2));
        mountainTiles.Add(new Vector2(14,2));
        mountainTiles.Add(new Vector2(15,2));
        mountainTiles.Add(new Vector2(4,3));
        mountainTiles.Add(new Vector2(6,3));
        mountainTiles.Add(new Vector2(4,4));
        mountainTiles.Add(new Vector2(6,4));
        mountainTiles.Add(new Vector2(7,4));
        mountainTiles.Add(new Vector2(8,4));
        mountainTiles.Add(new Vector2(4,5));
        mountainTiles.Add(new Vector2(0,6));
        mountainTiles.Add(new Vector2(1,6));
        mountainTiles.Add(new Vector2(2,6));
        mountainTiles.Add(new Vector2(11,6));
        mountainTiles.Add(new Vector2(13,6));
        mountainTiles.Add(new Vector2(14,6));
        mountainTiles.Add(new Vector2(15,6));
        mountainTiles.Add(new Vector2(0,7));
        mountainTiles.Add(new Vector2(9,7));
        mountainTiles.Add(new Vector2(10,7));
        mountainTiles.Add(new Vector2(11,7));
        mountainTiles.Add(new Vector2(15,7));
        mountainTiles.Add(new Vector2(5,8));
        mountainTiles.Add(new Vector2(6,8));


        tileType = 0;
        foreach (var position in mountainTiles){TerrainType[position] = tileType;}
    }

    void SetWaterTiles()
    {
        waterTiles = new List<Vector2>();
        
        waterTiles.Add(new Vector2(0,0));
        waterTiles.Add(new Vector2(1,0));
        waterTiles.Add(new Vector2(14,0));
        waterTiles.Add(new Vector2(15,0));
        waterTiles.Add(new Vector2(1,1));
        waterTiles.Add(new Vector2(2,1));
        waterTiles.Add(new Vector2(5,1));
        waterTiles.Add(new Vector2(6,1));
        waterTiles.Add(new Vector2(7,1));
        waterTiles.Add(new Vector2(13,1));
        waterTiles.Add(new Vector2(14,1));
        waterTiles.Add(new Vector2(5,2));
        waterTiles.Add(new Vector2(7,2));
        waterTiles.Add(new Vector2(5,3));
        waterTiles.Add(new Vector2(7,3));
        waterTiles.Add(new Vector2(8,3));
        waterTiles.Add(new Vector2(9,3));
        waterTiles.Add(new Vector2(10,3));
        waterTiles.Add(new Vector2(5,4));
        waterTiles.Add(new Vector2(9,4));
        waterTiles.Add(new Vector2(10,4));
        waterTiles.Add(new Vector2(5,5));
        waterTiles.Add(new Vector2(6,5));
        waterTiles.Add(new Vector2(7,5));
        waterTiles.Add(new Vector2(8,5));
        waterTiles.Add(new Vector2(9,5));
        waterTiles.Add(new Vector2(10,5));
        waterTiles.Add(new Vector2(9,6));
        waterTiles.Add(new Vector2(10,6));
        waterTiles.Add(new Vector2(1,7));
        waterTiles.Add(new Vector2(2,7));
        waterTiles.Add(new Vector2(4,7));
        waterTiles.Add(new Vector2(5,7));
        waterTiles.Add(new Vector2(6,7));
        waterTiles.Add(new Vector2(7,7));
        waterTiles.Add(new Vector2(13,7));
        waterTiles.Add(new Vector2(14,7));
        waterTiles.Add(new Vector2(0,8));
        waterTiles.Add(new Vector2(1,8));
        waterTiles.Add(new Vector2(4,8));
        waterTiles.Add(new Vector2(7,8));
        waterTiles.Add(new Vector2(14,8));
        waterTiles.Add(new Vector2(15,8));


        tileType = 2;
        foreach (Vector2 position in waterTiles){TerrainType[position] = tileType;}
    }
    
}
