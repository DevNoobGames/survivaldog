using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SquareFollow : MonoBehaviour
{
    public Grid grid;
    public float xOffset = 0.5f;
    public float yOffset = 0.5f;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int lPos = grid.WorldToCell(mousePos);
        this.transform.position = grid.CellToLocal(lPos);
        //this.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f);
        this.transform.position = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);
    }
}
