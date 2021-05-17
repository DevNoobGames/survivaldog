using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestOurTile : MonoBehaviour
{
	//HAVE TO SET THIS ONLY ONCE IN INSPECTOR / NOT EVERY TIME FOR EVERY TILE
	private WorldTile _tile;
	public Tile normalTile;
	public RuleTile replacementTile;
	public RuleTile farmTile;
	public PlayerScript player;
	public GameObject playerOBJ;
	public ToolbarScript toolbar;
	public GameObject square;

	public bool inRange;

	public bool isGrowing;
	public bool backToNormal;

	public int growthStage = 0;
	public RuleTile[] farmTiles;

	// Update is called once per frame
	private void Update()
	{
		//Gets location of mouse over tile
		Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
		var tiles = GameTiles.instance.tiles; // This is our Dictionary of tiles

		if (tiles.TryGetValue(worldPoint, out _tile)) //finds the specific tile
		{
			//USER ACTION
			if (Input.GetMouseButton(0) && inRange && player.horizontal == 0 && player.vertical == 0 && toolbar.slots[toolbar.ActiveItem].amount > 0 && !player.inUI) 
			{
				if (player.loaded)
				{
					player.loaded = false;
					player.startReload();

					//FARM
					if (player.isFarm && Input.GetMouseButtonDown(0)) 
                    {
						if (!checkIfTileHasObj(0, 0) && //check if each tile is clear
							!checkIfTileHasObj(0, 1) &&
							!checkIfTileHasObj(0, 2) &&
							!checkIfTileHasObj(-1, 0) &&
							!checkIfTileHasObj(-1, 1) &&
							!checkIfTileHasObj(-1, 2))
							{ //Replacement!
							replaceTileWithDig(0, 0);
							replaceTileWithDig(0, 1);
							replaceTileWithDig(0, 2);
							replaceTileWithDig(-1, 0);
							replaceTileWithDig(-1, 1);
							replaceTileWithDig(-1, 2);

							float randval = Random.value;
							if (randval < 0.5f)
							{
								Instantiate(Resources.Load("Humans/Farmer"), worldPoint, Quaternion.identity);
							}
							else
                            {
								Instantiate(Resources.Load("Humans/Farmer2"), worldPoint, Quaternion.identity);
							}

							toolbar.slots[toolbar.ActiveItem].amount -= 1;
							toolbar.UpdateStack();
							GameObject.FindGameObjectWithTag("digSound").GetComponent<AudioSource>().Play();

						}
						return;
					}

					//CHOPPING TREE
					if (_tile.HasTree && player.choppingPower > 0)
					{
						player.anim.SetBool("isDigging", true);
						_tile.ObjectHealth -= player.choppingPower;
						GameObject.FindGameObjectWithTag("digSound").GetComponent<AudioSource>().Play();
						if (_tile.ObjectHealth <= 0)
						{
							if (_tile.ObjectObj)
							{
								_tile.HasTree = false;
								Destroy(_tile.ObjectObj);
								dropSomething(1, 3, 6);
								GameObject.FindGameObjectWithTag("coinSound").GetComponent<AudioSource>().Play();

							}
						}
						return;
					}

					//ROCK CUTTING
					if (_tile.HasRock && player.axePower > 0)
					{
						player.anim.SetBool("isDigging", true);
						_tile.ObjectHealth -= player.axePower;
						GameObject.FindGameObjectWithTag("digSound").GetComponent<AudioSource>().Play();
						if (_tile.ObjectHealth <= 0)
						{
							if (_tile.ObjectObj)
							{
								_tile.HasRock = false;
								Destroy(_tile.ObjectObj);
								dropSomething(7, 3, 6);
								GameObject.FindGameObjectWithTag("coinSound").GetComponent<AudioSource>().Play();

							}
						}
						return;
					}

					//PLACE JELLYMAKER
					if (!_tile.IsDigged && toolbar.slots[toolbar.ActiveItem].itemInIdList == 5 && toolbar.slots[toolbar.ActiveItem].amount > 0 && !_tile.ObjectObj && Input.GetMouseButtonDown(0)) //5 is jellymaker
                    {
						_tile.IsDigged = true;
						_tile.ObjectObj = Instantiate(Resources.Load("Environment/jellyMaker"), new Vector2(_tile.WorldLocation.x + 0.5f, _tile.WorldLocation.y), Quaternion.identity) as GameObject;
						_tile.HasJellymaker = true;
						toolbar.RemoveItemFromToolbar(5, 1);
						GameObject.FindGameObjectWithTag("digSound").GetComponent<AudioSource>().Play();
					}

					//CREATING JELLY
					if (_tile.HasJellymaker && Input.GetMouseButtonDown(0) && toolbar.slots[toolbar.ActiveItem].itemInIdList == 6 && toolbar.slots[toolbar.ActiveItem].amount >= 5 && !_tile.isBrewing)
                    {
						_tile.isBrewing = true;
						toolbar.RemoveItemFromToolbar(6, 5);
						StartCoroutine(brewNum(_tile));
						GameObject.FindGameObjectWithTag("digSound").GetComponent<AudioSource>().Play();
					}

					//DIGGING
					if (!_tile.IsDigged && player.diggingPower > 0)
					{
						player.anim.SetBool("isDigging", true);
						if (square.transform.position.x < player.transform.position.x)
                        {
							Vector3 theScale = player.transform.localScale;
							theScale.x = 1.5f;
							player.transform.localScale = theScale;
						}
						else
                        {
							Vector3 theScale = player.transform.localScale;
							theScale.x = -1.5f;
							player.transform.localScale = theScale;
						}

						_tile.Health -= player.diggingPower;
						GameObject.FindGameObjectWithTag("digSound").GetComponent<AudioSource>().Play();
						if (_tile.Health <= 0)
						{
							float randval = Random.value;
							if (randval < 0.2f) //20% chance to drop bones
                            {
								Instantiate(Resources.Load("Environment/pui/pickupitem_Bone"), new Vector2(_tile.LocalPlace.x + 0.5f, _tile.LocalPlace.y + 0.5f), Quaternion.identity);
							}
							_tile.IsDigged = true;
							_tile.TilemapMember.SetTile(worldPoint, replacementTile);
							_tile.TilemapMember.SetTileFlags(_tile.LocalPlace, TileFlags.None);
							StartCoroutine(growBackToNormal(_tile, worldPoint));
						}
					}
				}
			}

			//create cursor
			if (player.range > 0) //Quick check to see if player has any range at all (Optimization so it doesn't have to calculate distance every time?
            {
				if (Vector2.Distance(new Vector2(_tile.WorldLocation.x + 0.5f, _tile.WorldLocation.y + 0.5f), new Vector2(playerOBJ.transform.position.x, playerOBJ.transform.position.y + 0.2f)) < player.range)
                {
					if (player.isFarm)
                    {
						setCursor(true);
						return;
                    }
					if (_tile.HasTree && player.choppingPower > 0)
					{
						setCursor(true);
						return;
					}
					else if (player.diggingPower > 0)
                    {
						setCursor(true);
						return;
					}
					else if (toolbar.slots[toolbar.ActiveItem].itemInIdList == 5 || toolbar.slots[toolbar.ActiveItem].itemInIdList == 6) //if jellymaker, for next time add an HASCURSOR bool at the player
                    {
						setCursor(true);
						return;
					}
					return;
				}
				else
                {
					setCursor(false);
                }
            }
			else
			{
				setCursor(false);
			}
		}

		if (Input.GetMouseButtonUp(0))
        {
			player.anim.SetBool("isDigging", false);
		}
	}

	public bool checkIfTileHasObj(float xPos, float yPos)
    {
		Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x + xPos), Mathf.FloorToInt(point.y + yPos), 0);
		var tiles = GameTiles.instance.tiles;
		if (tiles.TryGetValue(worldPoint, out _tile))
		{
			if (!_tile.ObjectObj && !_tile.isFarm) //&& !_tile.IsDigged
			{
				return false;
			}
			else
            {
				return true;
            }
		}
		else
        {
			return true;
        }
    }

	IEnumerator brewNum(WorldTile tile)
	{
		tile.ObjectObj.GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(10);
		Instantiate(Resources.Load("Environment/pui/pickupitem_Jam"), new Vector2(tile.LocalPlace.x + 1.5f, tile.LocalPlace.y + 0.5f), Quaternion.identity);
		tile.isBrewing = false;
		tile.ObjectObj.GetComponent<SpriteRenderer>().color = Color.white;
	}

	public void replaceTileWithDig(float xPos, float yPos)
    {
		Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x + xPos), Mathf.FloorToInt(point.y + yPos), 0);
		var tiles = GameTiles.instance.tiles;
		if (tiles.TryGetValue(worldPoint, out _tile))
		{
			_tile.Health = 0;
			_tile.IsDigged = true;
			_tile.isFarm = true;
			_tile.TilemapMember.SetTile(worldPoint, farmTiles[0]);
			StartCoroutine(growTile(_tile, worldPoint));
			_tile.TilemapMember.SetTileFlags(_tile.LocalPlace, TileFlags.None);
		}	
	}

	IEnumerator growBackToNormal(WorldTile tile, Vector3Int worldPoint)
    {
		yield return new WaitForSeconds(30);
		if (!tile.isFarm)
		{
			tile.TilemapMember.SetTile(worldPoint, normalTile);
			tile.IsDigged = false;
		}
		tile.Health = 100;
	}

	IEnumerator growTile(WorldTile tile, Vector3Int worldPoint)
    {
		yield return new WaitForSeconds(10);
		if (tile.growthStage < farmTiles.Length - 1)
		{
			tile.growthStage += 1;
		}
		else
        {
			Instantiate(Resources.Load("Environment/pui/pickupitem_Grape"), new Vector2(tile.LocalPlace.x + 0.5f, tile.LocalPlace.y + 0.5f), Quaternion.identity);
			tile.growthStage = 0;
        }
		tile.TilemapMember.SetTile(worldPoint, farmTiles[tile.growthStage]);
		StartCoroutine(growTile(tile, worldPoint));
	}

    public void setCursor(bool isInRange)
    {
		square.SetActive(isInRange);
		inRange = isInRange;
    }

    void dropSomething(int itemInIdList, int rangeMin, int rangeMax)
	{
		int dropAmount = Random.Range(rangeMin, rangeMax);
		toolbar.AddItemToToolbar(itemInIdList, dropAmount);
	}
}