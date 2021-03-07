using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float health;
    private float maxHealth;
    [SerializeField]
    private Slider mainTree_Progress;
    [SerializeField]
    private Text levelTxt;
    [SerializeField]
    private Text amtNeededTxt;

    private float amtNeeded;
    private float level;
    private bool pressingDown;
    private float dropActionRate;
    private float progress;
    private bool inTree;
    
    private MainTreeBuyDrop mainTree;
    
    // Start is called before the first frame update
    void Start()
    {
        mainTree_Progress.minValue = 0;
        mainTree_Progress.maxValue = 100;
        level = 1;
        amtNeeded = 3;
        mainTree = FindObjectOfType<MainTreeBuyDrop>();
        maxHealth = 100 * level;
    }

    // Update is called once per frame
    void Update()
    {
        
        pressingDown = Input.GetKey(KeyCode.E);
        mainTree_Progress.value = progress;
        levelTxt.text = "LVL: " + level.ToString();
        amtNeededTxt.text = amtNeeded.ToString();

        if (GameManager.manager.apple >= amtNeeded)
        {
            if(mainTree.level > level)
            {
                if (progress == 100)
                {
                    GameManager.manager.apple -= amtNeeded;
                    amtNeeded *= 2;
                    level++;
                    maxHealth = 100 * level;
                    health = maxHealth;
                    progress = 0;
                    dropActionRate = 1;

                }

                if (inTree)
                {
                    if (pressingDown)
                    {
                        if (dropActionRate <= 0)
                        {
                            progress++;
                            Debug.Log("interacting with tree from tree");
                        }
                    }


                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        progress = 0;
                        //dropActionRate = 1;
                    }
                }
            }
            
        }
    }

    private void FixedUpdate()
    {
        while (dropActionRate > 0)
        {
            dropActionRate -= Time.deltaTime;
        }
    }

    public void Take_Dmg(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            gameObject.SetActive(false);
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
            progress = 0;
        }
    }
}
