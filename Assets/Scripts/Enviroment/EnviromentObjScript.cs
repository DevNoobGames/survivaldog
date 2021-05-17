using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentObjScript : MonoBehaviour
{
    public enum ObjType { Palm, Stone, TBD, TBD2 }
    public ObjType TypeOfObj;

    public float health = 100;

    public GameObject selectSquare;

    public GameObject player;
    public PlayerScript playerscr;
    public ToolbarScript toolbar;

    public bool selected = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerscr = player.GetComponent<PlayerScript>();
        toolbar = GameObject.FindGameObjectWithTag("Toolbar").GetComponent<ToolbarScript>();

        if (selectSquare)
        {
            selectSquare.SetActive(false);
        }
    }

    private void OnMouseOver()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= playerscr.range)
        {
            selectSquare.SetActive(true);
            selected = true;
            //playerscr.selectedObj = gameObject;
        }
    }

    private void OnMouseExit()
    {
        disableIt();
    }

    public void gotHit()
    {
        if (TypeOfObj == ObjType.Palm)
        {
            health -= playerscr.choppingPower;
        }
        
        if (health <= 0)
        {
            int dropAmount = Random.Range(3, 6);
            if (TypeOfObj == ObjType.Palm)
            {
                Debug.Log("You got " + dropAmount + " wood");
                toolbar.AddItemToToolbar(1, dropAmount);
            }
            Destroy(gameObject);
        }
    }

    public void disableIt()
    {
        selectSquare.SetActive(false);
        selected = false;
        //playerscr.selectedObj = null;
    }

    void Update()
    {
        if (selected)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > playerscr.range)
            {
                disableIt();
            }
        }
    }


}
