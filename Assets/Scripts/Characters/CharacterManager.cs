using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Character character;
    private SpriteRenderer _spriteRenderer;
    public Tile OccupiedTile;
    public PlayerModeSelection PlayerMode;
    private TilePathSearch _tilePathSearch;

    public float Health;
    public int AttackPoints, MovementPoints;

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
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Move(Tile startingPoint, Tile endingPoint)
    {
        StartCoroutine(Movement(_tilePathSearch.Path(startingPoint, endingPoint)));
    }

    public void ReceiveDamage(float attackPoints)
    {
        float random = Random.Range(0f, 100f) / 100;
        float damage = attackPoints - (character.DefensePoints * random);
        Health -= damage;
        Debug.Log(random);
        Debug.Log("Received: " + damage + " of damage.");
    }

    public void Attack(CharacterManager enemy)
    {
        enemy.ReceiveDamage(character.AttackDamage);
    }

    public IEnumerator Movement(List<Tile> paths)
    {
        foreach (Tile path in paths) {
            Vector3 pos = path.transform.position;
            transform.position = new Vector3(pos.x, pos.y, -1);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
