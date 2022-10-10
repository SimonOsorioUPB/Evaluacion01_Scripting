using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Characters")]
public class Character : ScriptableObject
{
    public Sprite Sprite;
    public float Health;
    public int AttackPoints;
    public int MovementPoints;
    public int DefensePoints;
    public float AttackDamage;
    public float SpecialAttackDamage;
    public int SpecialAttackCost;
    
    public bool IsAHero;
    
}
[CustomEditor((typeof(Character)))]
public class CharacterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var character = target as Character;
        EditorGUILayout.ObjectField("Sprite", character.Sprite, typeof(Sprite),false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        character.Health = EditorGUILayout.FloatField("Health", character.Health);
        character.AttackPoints = EditorGUILayout.IntField("Attack Points", character.AttackPoints);
        character.MovementPoints = EditorGUILayout.IntField("Movement Points", character.MovementPoints);
        character.DefensePoints = EditorGUILayout.IntField("Defense Points", character.DefensePoints);
        character.AttackDamage = EditorGUILayout.FloatField("Attack Damage", character.AttackDamage);
        
        character.IsAHero = EditorGUILayout.Toggle("Is a Hero", character.IsAHero);
        
        if (character.IsAHero)
        {
            character.SpecialAttackDamage = EditorGUILayout.FloatField("Special Attack Damage", character.SpecialAttackDamage);
            character.SpecialAttackCost = EditorGUILayout.IntField("Special Attack Cost", character.SpecialAttackCost);
        }
    }
}
