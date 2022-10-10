using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<Character> _units;

    public CharacterManager SelectedUnit;
    private void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<Character>("Units").ToList();
    }

    public void SpawnUnits()
    {
        foreach (Character character in _units)
        {
            var spawnedUnit = Instantiate(character.prefab);
            Tile spawnTile = GridManager.Instance.GetTileAtPosition(character.position);
            spawnTile.OccupyingCharacter = spawnedUnit.GetComponent<CharacterManager>();
            spawnedUnit.GetComponent<CharacterManager>().OccupiedTile = spawnTile;
            spawnedUnit.transform.position = new Vector3(spawnTile.PosX, spawnTile.PosY, -1);
        }
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }

    public void SetSelectedUnit(CharacterManager unit)
    {
        SelectedUnit = unit;
    }
}
