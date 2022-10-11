using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField]
    private GameObject hero_AI_Panel, hero_Player_Panel, soldier1_AI_Panel, soldier1_Player_Panel, soldier2_AI_Panel, soldier2_Player_Panel;
    
    [SerializeField] 
    private CharacterManager hero_AI, hero_Player, soldier1_AI, soldier1_Player, soldier2_AI, soldier2_Player;
    [SerializeField] private TextMeshProUGUI hero_AI_health, hero_AI_AP, hero_AI_MP;
    [SerializeField] private TextMeshProUGUI hero_Player_health, hero_Player_AP, hero_Player_MP;
    [SerializeField] private TextMeshProUGUI soldier1_AI_health, soldier1_AI_AP, soldier1_AI_MP;
    [SerializeField] private TextMeshProUGUI soldier1_Player_health, soldier1_Player_AP, soldier1_Player_MP;
    [SerializeField] private TextMeshProUGUI soldier2_AI_health, soldier2_AI_AP, soldier2_AI_MP;
    [SerializeField] private TextMeshProUGUI soldier2_Player_health, soldier2_Player_AP, soldier2_Player_MP;

    private bool gotComponents;

    [Header("Menus")]
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject defeatScreen;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gotComponents = false;
        StartCoroutine(GetComponents());
    }

    IEnumerator GetComponents()
    {
        yield return new WaitForSeconds(1f);
        hero_AI = GameObject.Find("Hero (AI)(Clone)").GetComponent<CharacterManager>();
        hero_Player = GameObject.Find("Hero (Player)(Clone)").GetComponent<CharacterManager>();
        soldier1_AI = GameObject.Find("Soldier 1 (AI)(Clone)").GetComponent<CharacterManager>();
        soldier1_Player = GameObject.Find("Soldier 1 (Player)(Clone)").GetComponent<CharacterManager>();
        soldier2_AI = GameObject.Find("Soldier 2 (AI)(Clone)").GetComponent<CharacterManager>();
        soldier2_Player = GameObject.Find("Soldier 2 (Player)(Clone)").GetComponent<CharacterManager>();
        gotComponents = true;
    }

    private void Update()
    {
        if (gotComponents)
        {
            hero_AI_health.text = hero_AI.Health.ToString();
            hero_AI_AP.text = hero_AI.AttackPoints.ToString();
            hero_AI_MP.text = hero_AI.MovementPoints.ToString();
            hero_Player_health.text = hero_Player.Health.ToString();
            hero_Player_AP.text = hero_Player.AttackPoints.ToString();
            hero_Player_MP.text = hero_Player.MovementPoints.ToString();
            soldier1_AI_health.text = soldier1_AI.Health.ToString();
            soldier1_AI_AP.text = soldier1_AI.AttackPoints.ToString();
            soldier1_AI_MP.text = soldier1_AI.MovementPoints.ToString();
            soldier1_Player_health.text = soldier1_Player.Health.ToString();
            soldier1_Player_AP.text = soldier1_Player.AttackPoints.ToString();
            soldier1_Player_MP.text = soldier1_Player.MovementPoints.ToString();
            soldier2_AI_health.text = soldier2_AI.Health.ToString();
            soldier2_AI_AP.text = soldier2_AI.AttackPoints.ToString();
            soldier2_AI_MP.text = soldier2_AI.MovementPoints.ToString();
            soldier2_Player_health.text = soldier2_Player.Health.ToString();
            soldier2_Player_AP.text = soldier2_Player.AttackPoints.ToString();
            soldier2_Player_MP.text = soldier2_Player.MovementPoints.ToString();

            if (hero_AI == null) hero_AI_Panel.SetActive(false);
            if (soldier1_AI == null) soldier1_AI_Panel.SetActive(false);
            if (soldier2_AI == null) soldier2_AI_Panel.SetActive(false);
            if (hero_Player == null) hero_Player_Panel.SetActive(false);
            if (soldier1_Player == null) soldier1_Player_Panel.SetActive(false);
            if (soldier2_Player == null) soldier2_Player_Panel.SetActive(false);
        }
    }

    public void ShowDefeatScreen()
    {
        victoryScreen.SetActive(false);
        defeatScreen.SetActive(true);
    }

    public void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
        defeatScreen.SetActive(false);
    }
}
