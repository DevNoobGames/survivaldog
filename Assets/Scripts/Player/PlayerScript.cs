using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D body;
    [HideInInspector] public Animator anim;

    //movement
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    public float runSpeed = 20.0f;
    [HideInInspector] public bool inUI;

    //action
    public float range;
    public float damagePower;
    public float choppingPower;
    public float axePower;
    public float diggingPower;
    public bool loaded = true;
    public float reloadTime;
    public bool isFarm = false;
    //NEXT TIME ALSO ADD AN ISCURSOR BOOL!

    [Header("Other references")]
    public ToolbarScript toolbar;
    public MarkusScript markus;
    public TextMeshPro ownerText;
    public GameObject pauseMenu;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        /*toolbar.AddItemToToolbar(2, 1); //paws
        toolbar.AddItemToToolbar(10, 500); //money
        toolbar.AddItemToToolbar(1, 10);
        toolbar.AddItemToToolbar(3, 5);
        toolbar.AddItemToToolbar(4, 5);
        toolbar.AddItemToToolbar(5, 5);
        toolbar.AddItemToToolbar(6, 50);
        toolbar.AddItemToToolbar(7, 5);*/
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); //movement
        vertical = Input.GetAxisRaw("Vertical"); //movement

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }
    }
    
    public void menuButtonContinue()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }
    public void menuButtonQuit()
    {
        Application.Quit();
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed); //movement

        if (vertical < 0)
        {
            anim.SetBool("runningDown", true);
            anim.SetBool("runningUp", false);
            anim.SetBool("isDigging", false);
        }
        else if (vertical > 0)
        {
            anim.SetBool("runningUp", true);
            anim.SetBool("runningDown", false);
            anim.SetBool("isDigging", false);
        }
        else
        {
            anim.SetBool("runningDown", false);
            anim.SetBool("runningUp", false);
        }


        if (horizontal < 0)
        {
            anim.SetBool("runningHorizontal", true);
            anim.SetBool("isDigging", false);
            Vector3 theScale = transform.localScale;
            theScale.x = 1.5f;
            transform.localScale = theScale;

        }
        else if (horizontal > 0)
        {
            anim.SetBool("runningHorizontal", true);
            anim.SetBool("isDigging", false);
            Vector3 theScale = transform.localScale;
            theScale.x = -1.5f;
            transform.localScale = theScale;
        }
        else 
        {
            anim.SetBool("runningHorizontal", false);
        }
    }

    public void startReload()
    {
        StartCoroutine(reloading());
    }
    IEnumerator reloading()
    {
        yield return new WaitForSeconds(reloadTime);
        loaded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pui_bone"))
        {
            Destroy(collision.gameObject);
            toolbar.AddItemToToolbar(3, 1);
            GameObject.FindGameObjectWithTag("coinSound").GetComponent<AudioSource>().Play();
        }
        if (collision.CompareTag("pui_grape"))
        {
            Destroy(collision.gameObject);
            toolbar.AddItemToToolbar(6, 1);
            GameObject.FindGameObjectWithTag("coinSound").GetComponent<AudioSource>().Play();
        }
        if (collision.CompareTag("pui_jam"))
        {
            Destroy(collision.gameObject);
            toolbar.AddItemToToolbar(9, 1);
            GameObject.FindGameObjectWithTag("coinSound").GetComponent<AudioSource>().Play();
        }

        if (collision.CompareTag("owner"))
        {
            ownerText.text = "AHHHH YOU ARE BACKKK!!!!" + "\n" + "\n" + "Game is finished! Press ESC to quit. ";
            GameObject.FindGameObjectWithTag("talkSound").GetComponent<AudioSource>().Play();
        }
        if (collision.CompareTag("markusCollider"))
        {
            markus.missionRunner();
        }
    }
}

