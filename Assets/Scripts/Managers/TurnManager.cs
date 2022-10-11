using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    
    [HideInInspector] public CharacterManager Hero_AI;
    [HideInInspector] public CharacterManager Soldier1_AI;
    [HideInInspector] public CharacterManager Soldier2_AI;
    [HideInInspector] public CharacterManager Hero_Player;
    [HideInInspector] public CharacterManager Soldier1_Player;
    [HideInInspector] public CharacterManager Soldier2_Player;
    private bool gotComponents;

    public int HeroAIPoints, Soldier1AIPoints, Soldier2AIPoints;
    public int HeroPlayerPoints, Soldier1PlayerPoints, Soldier2PlayerPoints;

    public int AI_Points, Player_Points;

    [Header("Turn Sprites")] 
    [SerializeField]private GameObject yourTurnSprite;
    [SerializeField]private GameObject enemyTurnSprite;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gotComponents = false;
        StartCoroutine(GetComponents());
    }

    private void Update()
    {
        if (gotComponents)
        {
            if (Hero_AI != null) HeroAIPoints = Hero_AI.AttackPoints + Hero_AI.MovementPoints;
            else HeroAIPoints = 0;

            if (Hero_Player != null) HeroPlayerPoints = Hero_Player.AttackPoints + Hero_Player.MovementPoints;
            else HeroPlayerPoints = 0;

            if (Soldier1_AI != null) Soldier1AIPoints = Soldier1_AI.AttackPoints + Soldier1_AI.MovementPoints;
            else Soldier1AIPoints = 0;
            
            if (Soldier1_Player != null) Soldier1PlayerPoints = Soldier1_Player.AttackPoints + Soldier1_Player.MovementPoints;
            else Soldier1PlayerPoints = 0;
            
            if (Soldier2_AI != null) Soldier2AIPoints = Soldier2_AI.AttackPoints + Soldier2_AI.MovementPoints;
            else Soldier2AIPoints = 0;
            
            if (Soldier2_Player != null) Soldier2PlayerPoints = Soldier2_Player.AttackPoints + Soldier2_Player.MovementPoints;
            else Soldier2PlayerPoints = 0;

            Player_Points = HeroPlayerPoints + Soldier1PlayerPoints + Soldier2PlayerPoints;
            AI_Points = HeroAIPoints + Soldier1AIPoints + Soldier2AIPoints;

            if (Player_Points <= 0)
            {
                GameManager.Instance.UpdateGameState(GameState.EnemyTurn);
            }

            if (AI_Points <= 0)
            {
                GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
            }

            if (Hero_Player == null && Soldier1_Player == null && Soldier2_Player == null)
            {
                GameManager.Instance.UpdateGameState(GameState.Lose);
            }
            if (Hero_AI == null && Soldier1_AI == null && Soldier2_AI == null)
            {
                GameManager.Instance.UpdateGameState(GameState.Victory);
            }
        }
    }

    public void SetPlayerTurn()
    {
        enemyTurnSprite.SetActive(false);
        yourTurnSprite.SetActive(true);
    }
    public void SetEnemyTurn()
    {
        UIManager.Instance.HideHeroSpecialAttackButton();
        yourTurnSprite.SetActive(false);
        enemyTurnSprite.SetActive(true);
    }

    public void RestoreTurnPoints()
    {
        if (gotComponents)
        {
            Hero_AI.RestorePoints();
            Hero_Player.RestorePoints();
            Soldier1_AI.RestorePoints();
            Soldier1_Player.RestorePoints();
            Soldier2_AI.RestorePoints();
            Soldier2_Player.RestorePoints();
        }
    }

    IEnumerator GetComponents()
    {
        yield return new WaitForSeconds(1f);
        Hero_AI = GameObject.Find("Hero (AI)(Clone)").GetComponent<CharacterManager>();
        Hero_Player = GameObject.Find("Hero (Player)(Clone)").GetComponent<CharacterManager>();
        Soldier1_AI = GameObject.Find("Soldier 1 (AI)(Clone)").GetComponent<CharacterManager>();
        Soldier1_Player = GameObject.Find("Soldier 1 (Player)(Clone)").GetComponent<CharacterManager>();
        Soldier2_AI = GameObject.Find("Soldier 2 (AI)(Clone)").GetComponent<CharacterManager>();
        Soldier2_Player = GameObject.Find("Soldier 2 (Player)(Clone)").GetComponent<CharacterManager>();
        gotComponents = true;
    }

    public void HeroSpecialAttack()
    {
        Hero_Player.HeroSpecialAttack();
    }
}
