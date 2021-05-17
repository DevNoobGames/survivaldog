using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldTile //Used to be a MonoBehaviour
{
    public Vector3Int LocalPlace { get; set; }
    public Vector3 WorldLocation { get; set; }
    public TileBase TileBase { get; set; }
    public Tilemap TilemapMember { get; set; }
    public string Name { get; set; }

    // Below is needed for Breadth First Searching
    public bool isFarm { get; set; }
    public bool IsDigged { get; set; }
    public bool HasTree { get; set; }
    public bool HasRock { get; set; }
    public bool HasJellymaker { get; set; }
    public bool isBrewing { get; set; }
    public float ObjectHealth { get; set; }
    public WorldTile ExploredFrom { get; set; }
    public float Health { get; set; }
    public GameObject ObjectObj { get; set; }


    public int growthStage = 0;
}
