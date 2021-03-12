using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightMainTree : MonoBehaviour
{
    [SerializeField]
    private Sprite smile_highlight, not_smile_highlight, smile_not_highlight, not_smile_not_highlight;
    private SpriteRenderer spriterenderer;
    private bool inTree;
    private float smileCounter;
    // Start is called before the first frame update
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        smileCounter -= Time.deltaTime;
        if (inTree)
        {
            if(smileCounter > 0)
            {
                spriterenderer.sprite = smile_highlight;
            }
            else
            {
                spriterenderer.sprite = not_smile_highlight;
            }
        }
        else
        {
            if (smileCounter > 0)
            {
                spriterenderer.sprite = smile_not_highlight;
            }
            else
            {
                spriterenderer.sprite = not_smile_not_highlight;
            }
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTree = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTree = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTree = false;
        }
    }
}
