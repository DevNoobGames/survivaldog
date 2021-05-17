using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject panel;
    public GameObject credits;
    public GameObject airplane1;
    public GameObject airplane2;
    public GameObject particleSys;
    public GameObject airplane3;

    public void startBtn()
    {
        panel.SetActive(false);
        StartCoroutine(introSequence());
        GetComponent<AudioSource>().Stop();
    }

    public void quitBtn()
    {
        Application.Quit();
    }

    public void creditsBTN()
    {
        credits.SetActive(!credits.activeInHierarchy);
    }

    IEnumerator introSequence()
    {
        airplane1.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        airplane1.SetActive(false);
        airplane2.SetActive(true);
        yield return new WaitForSeconds(4);
        particleSys.SetActive(true);
        yield return new WaitForSeconds(4);
        airplane2.SetActive(false);
        particleSys.SetActive(false);
        airplane3.SetActive(true);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
