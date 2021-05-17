using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameTiles : MonoBehaviour
{
	public static GameTiles instance;
	public Tilemap Tilemap;

	public Dictionary<Vector3, WorldTile> tiles;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		GetWorldTiles();
	}

	// Use this for initialization
	private void GetWorldTiles()
	{
		tiles = new Dictionary<Vector3, WorldTile>();
		foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin)
		{
			var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

			if (!Tilemap.HasTile(localPlace)) continue;
			var tile = new WorldTile
			{
				LocalPlace = localPlace,
				WorldLocation = Tilemap.CellToWorld(localPlace),
				TileBase = Tilemap.GetTile(localPlace),
				TilemapMember = Tilemap,
				Name = localPlace.x + "," + localPlace.y,
				Health = 100, // TODO: Change this with the proper cost from ruletile
		};

			tiles.Add(tile.WorldLocation, tile);
			float randval = Random.value;
			if (randval < 0.1f)
			{
				float randval2 = Random.value;
				if (randval2 < 0.7f)
				{
					tile.ObjectObj = Instantiate(Resources.Load("Environment/Tree"), new Vector2(tile.WorldLocation.x + 0.5f, tile.WorldLocation.y + 0.5f), Quaternion.identity) as GameObject;
					tile.HasTree = true;
					tile.ObjectHealth = 100;
				}
				else
                {
					tile.ObjectObj = Instantiate(Resources.Load("Environment/Rock"), new Vector2(tile.WorldLocation.x + 0.5f, tile.WorldLocation.y), Quaternion.identity) as GameObject;
					tile.HasRock = true;
					tile.ObjectHealth = 100;
				}
			}
		}
	}
}
