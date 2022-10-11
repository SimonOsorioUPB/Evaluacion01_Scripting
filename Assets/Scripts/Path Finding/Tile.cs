using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Tile : MonoBehaviour
{
    [SerializeField] private Color _groundColor, _mountainColor, _waterColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;
    public CharacterManager OccupyingCharacter;

    public bool Walkable => _isWalkable && OccupyingCharacter == null;

    public int tileType = 1; // 0 = Mountain, 1 = Ground, 2 = Water
    public int PosX, PosY;
    
    ///<BFS>
    public bool isExplored = false;
    public Tile isExploredFrom;
    public Vector2Int GetPos(){ return new Vector2Int(PosX, PosY); }
    ///<BFS>

    public void Init(int type)
    {
        tileType = 1;
        tileType = type;
        if (type == 0){_renderer.color = _mountainColor; _isWalkable = false;}
        else if (type == 1){_renderer.color = _groundColor; _isWalkable = true;}
        else if (type == 2){_renderer.color = _waterColor; _isWalkable = true;}
    }   
 
    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }
 
    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.State != GameState.PlayerTurn) return;
        if (OccupyingCharacter != null)
        {
            if (OccupyingCharacter.PlayerMode == PlayerModeSelection.Player) UnitManager.Instance.SetSelectedUnit(OccupyingCharacter);
            else
            {
                if (UnitManager.Instance.SelectedUnit != null)
                {
                    //Attack
                    UnitManager.Instance.SelectedUnit.Attack(OccupyingCharacter);
                    UnitManager.Instance.SetSelectedUnit(null);
                }
            }
        }
        else
        {
            if (UnitManager.Instance.SelectedUnit != null)
            {
                //Move Unit
                if (Walkable)
                {
                    Debug.Log("Moves");
                    UnitManager.Instance.SelectedUnit.Move(UnitManager.Instance.SelectedUnit.OccupiedTile, this);
                    UnitManager.Instance.SelectedUnit.OccupiedTile.OccupyingCharacter = null;
                    UnitManager.Instance.SelectedUnit.OccupiedTile = this;
                    OccupyingCharacter = UnitManager.Instance.SelectedUnit;
                    UnitManager.Instance.SetSelectedUnit(null);
                }
            }
        }
    }

    public void SetUnit(CharacterManager unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupyingCharacter = null;
        unit.transform.position = new Vector3(PosX, PosY, -1);
        OccupyingCharacter = unit;
        unit.OccupiedTile = this;
    }
}
