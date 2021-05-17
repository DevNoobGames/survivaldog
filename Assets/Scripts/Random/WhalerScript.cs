using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WhalerScript : MonoBehaviour
{
    public GameObject[] ObjectsToDeleteOnPayment;
    public ToolbarScript toolbar;
    public TextMeshPro whalerText;
    public TextMeshProUGUI missionText;
    public int opened;

    private void Start()
    {
        opened = 1;
    }

    private void OnMouseDown()
    {
        if (opened == 1)
        {
            whalerText.text = "$420,69 to pass plz";
            GameObject.FindGameObjectWithTag("talkSound").GetComponent<AudioSource>().Play();
            missionText.text = "Find a way to make money";
            opened = 2;
            return;
        }

        for (int i = 0; i < toolbar.slots.Length; i++) //run all slots to check if we already have it
        {
            if (toolbar.slots[i].itemInIdList == 10 && toolbar.slots[i].amount >= 420 && opened == 2)
            {
                opened = 3;
                StartCoroutine(removeStuff());
                return;
            }
        }
        if (opened == 2)
        {
            whalerText.text = "GIVE ME MONEY FIRST! $420,69!!!";
            GameObject.FindGameObjectWithTag("talkSound").GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator removeStuff()
    {
        whalerText.text = "Thank you little doggie. Give me a moment while I remove the chairs...";
        GameObject.FindGameObjectWithTag("talkSound").GetComponent<AudioSource>().Play();
        toolbar.RemoveItemFromToolbar(10, 420);
        yield return new WaitForSeconds(6);
        foreach (GameObject item in ObjectsToDeleteOnPayment)
        {
            Destroy(item);
        }
        whalerText.text = "Enjoy!";
        GameObject.FindGameObjectWithTag("talkSound").GetComponent<AudioSource>().Play();
    }
}
