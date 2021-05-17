using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellymakerScript : MonoBehaviour
{
    public ToolbarScript toolbar;
    public bool brewing = false;
    public TestOurTile tileRange;

    private void Start()
    {
        toolbar = GameObject.FindGameObjectWithTag("Toolbar").GetComponent<ToolbarScript>();
        tileRange = GameObject.FindGameObjectWithTag("tileManager").GetComponent<TestOurTile>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (toolbar.slots[toolbar.ActiveItem].itemInIdList == 6 && toolbar.slots[toolbar.ActiveItem].amount > 0 && !brewing && tileRange.inRange) //grapes
                {
                    brewing = true;
                    toolbar.RemoveItemFromToolbar(6, 1);
                    StartCoroutine(brewNum());
                }
            }
        }
    }

    IEnumerator brewNum()
    {
        yield return new WaitForSeconds(6);
        Debug.Log("Good job");
        brewing = false;
    }
}
