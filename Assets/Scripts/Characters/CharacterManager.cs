using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Character character;
    private SpriteRenderer _spriteRenderer;
    public Tile OccupiedTile;
    public PlayerModeSelection PlayerMode;
    private TilePathSearch _tilePathSearch;
    public GameObject SelectionSprite;

    public bool IsAHero;

    public float Health;
    public int AttackPoints, MovementPoints;

    public bool IsOnAttackRange, TriggerHeroSpecialAttack;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = character.Sprite;
        PlayerMode = character.PlayerMode;
        _tilePathSearch = TilePathSearch.Instance;
        _tilePathSearch.LoadAllBlocks();
        Health = character.Health;
        AttackPoints = character.AttackPoints;
        MovementPoints = character.MovementPoints;
        IsOnAttackRange = false;
        TriggerHeroSpecialAttack = false;
        IsAHero = character.IsAHero;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }

        if (MovementPoints <= 0 && !IsOnAttackRange)
        {
            AttackPoints = 0;
        }
    }

    public void RestorePoints()
    {
        AttackPoints = character.AttackPoints;
        MovementPoints = character.MovementPoints;
    }

    public bool MoveCheck(Tile startingPoint, Tile endingPoint)
    {
        List<Tile> paths = _tilePathSearch.Path(startingPoint, endingPoint);
        if (paths.Count - 1 <= MovementPoints)
        {
            StartCoroutine(Movement(paths));
            MovementPoints -= (paths.Count - 1);
            return true;
        }
        else
        {
            Debug.Log("This unit doesn't have the required Movement Points to reach this tile");
            Debug.Log("You can move " + MovementPoints + " tiles.");
            return false;
        }
    }

    public void ReceiveDamage(float attackPoints)
    {
        float random = Random.Range(0f, 100f) / 100;
        float damage = attackPoints - (character.DefensePoints * random);
        Health -= (int) damage;
        Debug.Log(random);
        Debug.Log("Received: " + damage + " of damage.");
    }

    public void Attack(CharacterManager enemy)
    {
        if (AttackPoints >= 0)
        {
            enemy.ReceiveDamage(character.AttackDamage);
            AttackPoints -= 1;
        }
    }

    public void HeroSpecialAttack()
    {
        TriggerHeroSpecialAttack = true;
    }

    public IEnumerator Movement(List<Tile> paths)
    {
        foreach (Tile path in paths) {
            Vector3 pos = path.transform.position;
            transform.position = new Vector3(pos.x, pos.y, -1);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CharacterManager collidedCharacter = collision.gameObject.GetComponent<CharacterManager>();
        if (collision.gameObject.CompareTag("Character"))
        {
            if (PlayerMode == PlayerModeSelection.Player && collidedCharacter.PlayerMode == PlayerModeSelection.AI) IsOnAttackRange = true;
            else if (PlayerMode == PlayerModeSelection.AI && collidedCharacter.PlayerMode == PlayerModeSelection.Player) IsOnAttackRange = true;
            else{ IsOnAttackRange = false; }
        }

        Debug.Log("On Range " + collision);

        if (IsOnAttackRange && TriggerHeroSpecialAttack)
        {
            TriggerHeroSpecialAttack = false;
            if (AttackPoints >= 0)
            {
                collidedCharacter.ReceiveDamage(character.SpecialAttackDamage);
                AttackPoints -= character.SpecialAttackCost;
            }
        }
    }
}
