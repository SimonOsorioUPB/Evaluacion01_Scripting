using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    
    public Dictionary<Vector2, int> TerrainType;

    private List<Vector2> waterTiles, groundTiles, mountainTiles;
    private int tileType = 1; // 0 = Mountain, 1 = Ground, 2 = Water

    private void Awake()
    {
        SetAllTiles();
        SetMountainTiles();
        SetGroundTiles();
        SetWaterTiles();
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
        
        mountainTiles.Add(new Vector2(0,0));
        mountainTiles.Add(new Vector2(5,5));

        
        
        tileType = 0;
        foreach (var position in mountainTiles){TerrainType[position] = tileType;}
    }
    
    void SetGroundTiles()
    {
        groundTiles = new List<Vector2>();
        
        groundTiles.Add(new Vector2(1,1));
        
        
        tileType = 1;
        foreach (Vector2 position in groundTiles){TerrainType[position] = tileType;}
    }
    
    void SetWaterTiles()
    {
        waterTiles = new List<Vector2>();
        
        waterTiles.Add(new Vector2(2,2));
        waterTiles.Add(new Vector2(2,3));
        waterTiles.Add(new Vector2(3,2));
        waterTiles.Add(new Vector2(3,3));


        tileType = 2;
        foreach (Vector2 position in waterTiles){TerrainType[position] = tileType;}
    }
    
}
