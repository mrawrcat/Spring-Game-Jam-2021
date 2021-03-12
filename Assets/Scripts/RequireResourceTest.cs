using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequireResourceTest : MonoBehaviour
{
    [SerializeField]
    private bool inBlock;
    public int level;
    private float reqApple;
    private float reqDewdrop;
    [SerializeField]
    private Text lvltxt;
    public bool CanAfford()
    {
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        reqDewdrop = 1;
        reqApple = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lvltxt.text = "Level: " + level.ToString();
        crappyAmtReq();
        if (inBlock)
        {
            if(level < 3)
            {
                Interact();
            }
        }
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.manager.apple >= reqApple && GameManager.manager.Spring_Dewdrop >= reqDewdrop)
            {
                GameManager.manager.apple -= reqApple;
                GameManager.manager.Spring_Dewdrop -= reqDewdrop;
                level++;
            }
        }
    }
    private void crappyAmtReq()
    {
        if(level + 1 == 1)
        {
            reqDewdrop = 1;
            reqApple = 0;
            
        }
        else if (level + 1 == 2)
        {
            reqDewdrop = 5;
            reqApple = 1;
            
        }
        else if (level + 1 == 3)
        {
            reqDewdrop = 10;
            reqApple = 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inBlock = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inBlock = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inBlock = false;
        }
    }

}
