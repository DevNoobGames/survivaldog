using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarkusScript : MonoBehaviour
{

    public int activeMission;
    public ToolbarScript toolbar;
    public GameObject craftingMenu;
    public GameObject craftingBook;
    public PlayerScript player;
    public TextMeshPro markusText;
    public WhalerScript whaler;
    public GameObject Markus;

    void Start()
    {
        activeMission = 0;
    }

    public void missionRunner()
    {
        if (whaler.opened >= 2 && activeMission == 0)
        {
            activeMission = 1;
            toolbar.AddItemToToolbar(2, 1);
            GameObject.FindGameObjectWithTag("coinSound").GetComponent<AudioSource>().Play();
            Markus.SetActive(true);
            return;
        }

        if (activeMission == 1)
        {
            for (int i = 0; i < toolbar.slots.Length; i++) //run all slots to check if we have it
            {
                if (toolbar.slots[i].itemInIdList == 3) //bones
                {
                    if (toolbar.slots[i].amount >= 5) //if we have 5 of em
                    {
                        toolbar.AddItemToToolbar(4, 2);
                        GameObject.FindGameObjectWithTag("coinSound").GetComponent<AudioSource>().Play();
                        toolbar.RemoveItemFromToolbar(3, 5);
                        markusText.text = "Good doggie. Come back to exchange stuff";
                        GameObject.FindGameObjectWithTag("talkSound").GetComponent<AudioSource>().Play();
                        activeMission = 2;
                        return; //done
                    }
                }
            }
        }
        else if (activeMission != 0)
        {
            craftingMenu.SetActive(true);
            craftingBook.SetActive(true);
            player.inUI = true;
            Time.timeScale = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            craftingMenu.SetActive(false);
            craftingBook.SetActive(false);
            player.inUI = false;
            Time.timeScale = 1;
        }
    }
}
