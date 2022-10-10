using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Character character;
    private SpriteRenderer _spriteRenderer;
    public Tile OccupiedTile;
    public PlayerModeSelection PlayerMode;
    [SerializeField] private TilePathSearch _tilePathSearch;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = character.Sprite;
        PlayerMode = character.PlayerMode;
        _tilePathSearch = Instantiate(_tilePathSearch);
        _tilePathSearch.LoadAllBlocks();
    }

    public void Move(Tile startingPoint, Tile endingPoint)
    {
        _tilePathSearch = Instantiate(_tilePathSearch);
        StartCoroutine(Movement(_tilePathSearch.Path(startingPoint, endingPoint)));
    }

    public IEnumerator Movement(List<Tile> paths)
    {
        foreach (Tile path in paths) {
            Vector3 pos = path.transform.position;
            transform.position = new Vector3(pos.x, pos.y, -1);
            yield return new WaitForSeconds(0.2f);
        }
        _tilePathSearch._path = null;
        //Destroy(_tilePathSearch);
    }
}
