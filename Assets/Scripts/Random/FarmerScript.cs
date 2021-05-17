using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerScript : MonoBehaviour
{
    public float xStart;
    public float xEnd;
    public float yStart;
    public float yEnd;
    public Vector2 targetPos;

    [Header ("Sprites")]
    public Sprite forwardSpr;
    public Sprite leftSpr;
    public Sprite downSpr;
    public float spriteSize = 5;


    void Start()
    {
        targetPos = transform.position;

        xStart = transform.position.x;
        yStart = transform.position.y;

        InvokeRepeating("findPos", 1, 6);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, 3 * Time.deltaTime);

       
    }

    public void findPos()
    {
        float randY = Random.Range(yStart, yStart + yEnd);
        float randX = Random.Range(xStart, xStart + xEnd);
        targetPos = new Vector2(randX, randY);
        float Angle = AngleBetweenVector2(transform.position, targetPos);
        
        if (Angle > 0)
        {
            if (Angle < 45)
            {
                //Debug.Log("right");
                GetComponent<SpriteRenderer>().sprite = leftSpr;
                Vector3 theScale = transform.localScale;
                theScale.x = -spriteSize;
                transform.localScale = theScale;
            }
            else if (Angle < 135)
            {
                //Debug.Log("up");
                GetComponent<SpriteRenderer>().sprite = forwardSpr;
            }
            else
            {
                //Debug.Log("left");
                GetComponent<SpriteRenderer>().sprite = leftSpr;
                Vector3 theScale = transform.localScale;
                theScale.x = spriteSize;
                transform.localScale = theScale;
            }
        }
        else
        {
            if (Angle > -45)
            {
                //Debug.Log("Right");
                GetComponent<SpriteRenderer>().sprite = leftSpr;
                Vector3 theScale = transform.localScale;
                theScale.x = -spriteSize;
                transform.localScale = theScale;
            }
            else if (Angle > -135)
            {
                //Debug.Log("down");
                GetComponent<SpriteRenderer>().sprite = downSpr;
            }
            else
            {
                //Debug.Log("left");
                GetComponent<SpriteRenderer>().sprite = leftSpr;
                Vector3 theScale = transform.localScale;
                theScale.x = spriteSize;
                transform.localScale = theScale;
            }
        }


    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
}
