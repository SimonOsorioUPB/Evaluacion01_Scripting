using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GridManager : MonoBehaviour
{
    public int Width, Height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private TerrainManager _terrainManager;
 
    public Dictionary<Vector2, Tile> _tiles;

    void Start()
    {
        StartCoroutine(GenerateGrid());
    }
 
    IEnumerator GenerateGrid()
    {
        yield return new WaitForSeconds(1f);
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.posX = x;
                spawnedTile.posY = y;
                spawnedTile.name = $"Tile {x},{y}";
 
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(_terrainManager.TerrainType[new Vector2(x, y)]);

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
 
        _cam.transform.position = new Vector3((float)Width/2 -0.5f, (float)Height / 2 - 0.5f,-10);
    }
 
    public Tile GetTileAtPosition(Vector2 pos) {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}