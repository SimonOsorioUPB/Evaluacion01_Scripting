using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.GenerateGrid);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.GenerateGrid:
                StartCoroutine(GridManager.Instance.GenerateGrid());
                break;
            case GameState.SpawnUnits:
                UnitManager.Instance.SpawnUnits();
                break;
            case GameState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case GameState.EnemyTurn:
                HandleEnemyTurn();
                break;
            case GameState.Victory:
                UIManager.Instance.ShowVictoryScreen();
                break;
            case GameState.Lose:
                UIManager.Instance.ShowDefeatScreen();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        } 

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandlePlayerTurn()
    {
        TurnManager.Instance.RestoreTurnPoints();
        TurnManager.Instance.SetPlayerTurn();
    }

    private void HandleEnemyTurn()
    {
        TurnManager.Instance.RestoreTurnPoints();
        TurnManager.Instance.SetEnemyTurn();
    }
}

public enum GameState
{
    GenerateGrid,
    SpawnUnits,
    PlayerTurn,
    EnemyTurn,
    Victory,
    Lose
}