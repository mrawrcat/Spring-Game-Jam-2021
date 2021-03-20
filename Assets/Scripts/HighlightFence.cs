using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightFence : MonoBehaviour
{
    [SerializeField]
    private Sprite full_highlight, half_highlight, dead_highlight, full_no_highlight, half_no_highlight, dead_no_highlight;
    private SpriteRenderer spriterenderer;
    private bool inFence;
    private Fence fence;
    [SerializeField]
    private float health;
    private float reqAmt; //for repairing
    [SerializeField]
    private bool touchingVarmint;
    private float dewReqAmt; //for upgrading
    private float appleReqAmt;
    [SerializeField]
    private GameObject UIstuff;
    [SerializeField]
    private Text lvlTxt;
    [SerializeField]
    private Text dewReqText;
    [SerializeField]
    private Text appleReqText;
    [SerializeField]
    private Slider healthSlider;
    private int lvl;
    private float regenRate;
    // Start is called before the first frame update
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        fence = GetComponentInParent<Fence>();
        reqAmt = 1;
        dewReqAmt = 1;
        appleReqAmt = 1;
        lvl = 1;
        regenRate = 1f;
        healthSlider.minValue = 0;
        healthSlider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health;
        lvlTxt.text = lvl.ToString();
        dewReqText.text = dewReqAmt.ToString("F0");
        appleReqText.text = appleReqAmt.ToString("F0");
        health = fence.health;

        if(fence.health > 0 && fence.health < 100)
        {
            fence.health += regenRate * Time.deltaTime;
        }
        else if(fence.health >= 100)
        {
            fence.health = 100;
        }

        if (inFence)
        {

            UIstuff.SetActive(true);

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



            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(health > 0)
                {
                    if(GameManager.manager.dewdrop >= dewReqAmt && GameManager.manager.apple >= appleReqAmt)
                    {
                        lvl++;
                        GameManager.manager.dewdrop -= dewReqAmt;
                        GameManager.manager.apple -= appleReqAmt;
                        dewReqAmt += 2;
                        appleReqAmt += 1;
                        regenRate += 1f;

                    }
                }
            }

            
            
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(health < 100 && health + 25 <= 100 && health != 0)
                {
                    if(GameManager.manager.dewdrop >= reqAmt)
                    {
                        GameManager.manager.dewdrop -= reqAmt;
                        fence.health += 25;
                    }
                }
                else if(health + 25 > 100 && health !> 100 && health != 0)
                {
                    if(GameManager.manager.dewdrop >= reqAmt)
                    {
                        GameManager.manager.dewdrop -= reqAmt;
                        fence.health = 100;
                    }
                    
                }
                else if (health <=  0)
                {
                    if (!touchingVarmint)
                    {
                        if (GameManager.manager.dewdrop >= reqAmt)
                        {
                            GameManager.manager.dewdrop -= reqAmt;
                            fence.health += 25;
                        }
                    }
                }
            }

        }
        else
        {
            UIstuff.SetActive(false);
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
