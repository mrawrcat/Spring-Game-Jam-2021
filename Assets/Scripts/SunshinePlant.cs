using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunshinePlant : MonoBehaviour
{
    
    [SerializeField]
    private Sprite not_fed_not_highlighted;
    [SerializeField]
    private Sprite not_fed_highlighted;
    [SerializeField]
    private Sprite fed_not_highlighted;
    [SerializeField]
    private float fullTime; //time that flower stays fed
    [SerializeField]
    private Transform dropPoint;
    [SerializeField]
    private PetalPool petalPool;
    [SerializeField]
    private float force;
    private bool fed;
    private float health;
    private bool inside;
    private SpriteRenderer spriterenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inside)
        {
            if (fed)
            {
                spriterenderer.sprite = fed_not_highlighted;
            }
            else
            {
                spriterenderer.sprite = not_fed_highlighted;
            }
            Interact();
            
        }
        else
        {
            if (fed)
            {
                spriterenderer.sprite = fed_not_highlighted;
            }
            else
            {
                spriterenderer.sprite = not_fed_not_highlighted;
            }
        }

        if(health <= 0)
        {
            fed = false;
        }
        else
        {
            fed = true;
        }
       
    }

    private void FixedUpdate()
    {
        if (health > 0)
        {
            health -= 1 * Time.deltaTime;
        }
    }

    private void Interact()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!fed)
            {
                health = fullTime;
                petalPool.SpawnPetal(dropPoint, force);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inside = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inside = false;
        }
    }
}
