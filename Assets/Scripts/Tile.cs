using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Tile : MonoBehaviour
{
    [SerializeField] private Color _groundColor, _mountainColor, _waterColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public int tileType = 1; // 0 = Mountain, 1 = Ground, 2 = Water

    public bool IsWalkable;
    public int posX, posY;
 
    public void Init(int type)
    {
        tileType = 1;
        tileType = type;
        if (type == 0){_renderer.color = _mountainColor;}
        else if (type == 1){_renderer.color = _groundColor;}
        else if (type == 2){_renderer.color = _waterColor;}
    }   
 
    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }
 
    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
}
