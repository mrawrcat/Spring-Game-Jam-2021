using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightFence : MonoBehaviour
{
    [SerializeField]
    private Sprite full_highlight, half_highlight, dead_highlight, full_no_highlight, half_no_highlight, dead_no_highlight;
    private SpriteRenderer spriterenderer;
    private bool inFence;
    private float health; //later get this from parent object script
    // Start is called before the first frame update
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        health = GetComponentInParent<Fence>().health;
        if (inFence)
        {
            if(health >= 100)
            {
                spriterenderer.sprite = full_no_highlight;
            }
            else if (health > 50)
            {
                spriterenderer.sprite = full_highlight;
            }
            else if (health <= 50 && health > 0)
            {
                spriterenderer.sprite = half_highlight;
            }
            else
            {
                spriterenderer.sprite = dead_highlight;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(health < 100 && health +25 <= 100)
                {
                    GetComponentInParent<Fence>().health += 25;
                }
                else if(health + 25 > 100)
                {
                    GetComponentInParent<Fence>().health = 100;
                }
            }
        }
        else
        {
            if (health > 50)
            {
                spriterenderer.sprite = full_no_highlight;
            }
            else if (health <= 50 && health > 0)
            {
                spriterenderer.sprite = half_no_highlight;
            }
            else
            {
                spriterenderer.sprite = dead_no_highlight;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inFence = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inFence = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inFence = false;
        }
    }
}
