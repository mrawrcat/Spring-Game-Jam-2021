using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField]
    private Sprite not_highlighted, highlighted;
    private SpriteRenderer spriterenderer;
    private bool something_entered;
    // Start is called before the first frame update
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        spriterenderer.sprite = not_highlighted;
    }

    // Update is called once per frame
    void Update()
    {
        if (something_entered)
        {
            spriterenderer.sprite = highlighted;
        }
        else
        {
            spriterenderer.sprite = not_highlighted;
        }
    }

    

    private void OnMouseOver()
    {
        //spriterenderer.sprite = highlighted;
        //something_entered = true;
    }

    private void OnMouseExit()
    {
        //spriterenderer.sprite = not_highlighted;
        //something_entered = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            something_entered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            something_entered = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            something_entered = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            something_entered = false;
        }
    }
}
