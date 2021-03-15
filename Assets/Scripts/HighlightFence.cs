using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightFence : MonoBehaviour
{
    [SerializeField]
    private Sprite full_highlight, half_highlight, dead_highlight, full_no_highlight, half_no_highlight, dead_no_highlight;
    private SpriteRenderer spriterenderer;
    private bool inFence;
    private Fence fence;
    [SerializeField]
    private float health;
    private float reqAmt;
    [SerializeField]
    private bool touchingVarmint;
    // Start is called before the first frame update
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        fence = GetComponentInParent<Fence>();
    }

    // Update is called once per frame
    void Update()
    {
        crappyReq();
        health = fence.health;
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
                if(health < 100 && health + 25 <= 100 && health != 0)
                {
                    if(GameManager.manager.Spring_Dewdrop >= reqAmt)
                    {
                        GameManager.manager.Spring_Dewdrop -= reqAmt;
                        fence.health += 25;
                    }
                }
                else if(health + 25 > 100 && health !> 100 && health != 0)
                {
                    if(GameManager.manager.Spring_Dewdrop >= reqAmt)
                    {
                        GameManager.manager.Spring_Dewdrop -= reqAmt;
                        fence.health = 100;
                    }
                    
                }
                else if (health <=  0)
                {
                    if (!touchingVarmint)
                    {
                        if (GameManager.manager.Spring_Dewdrop >= reqAmt)
                        {
                            GameManager.manager.Spring_Dewdrop -= reqAmt;
                            fence.health += 25;
                        }
                    }
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

    private void crappyReq()
    {
        if(health <= 0)
        {
            reqAmt = 5;
        }
        else
        {
            reqAmt = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inFence = true;
        }
        if(collision.tag == "Enemy")
        {
            touchingVarmint = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inFence = true;
        }
        if (collision.tag == "Enemy")
        {
            touchingVarmint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inFence = false;
        }
        if (collision.tag == "Enemy")
        {
            touchingVarmint = false;
        }
    }
}
