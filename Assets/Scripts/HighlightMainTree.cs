using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightMainTree : MonoBehaviour
{
    private enum State
    {
        NotSpawning,
        Spawning,
    }
    private State state;
    
    [SerializeField]
    private Sprite smile_highlight, not_smile_highlight, smile_not_highlight, not_smile_not_highlight;
    private SpriteRenderer spriterenderer;
    private bool inTree;
    [SerializeField]
    private PetalPool petalPool;
    private float nextSpawnTimer;
    private int remainingSpawnAmt;
    [SerializeField]
    private float spawnforce;
    [SerializeField]
    private int amtToSpawn;
    [SerializeField]
    private float spawnRate;
    private float appleReqAmt;
    private float pollenReqAmt;
    private int lvl;
    private float mainTreeMaxHealth;
    [SerializeField]
    private Slider healthbar;
    [SerializeField]
    private Text lvlTxt;
    [SerializeField]
    private Text appleReqText;
    [SerializeField]
    private Text pollenReqText;
    [SerializeField]
    private GameObject UIstuff;
    // Start is called before the first frame update
    private void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        state = State.NotSpawning;
        amtToSpawn = 3;
        appleReqAmt = 3;
        pollenReqAmt = 0;
        healthbar.minValue = 0;
        mainTreeMaxHealth = 100;
        lvl = 1;

    }

    // Update is called once per frame
    private void Update()
    {

        lvlTxt.text = lvl.ToString();
        healthbar.maxValue = mainTreeMaxHealth;
        healthbar.value = GameManager.manager.Main_Tree_Health;
        appleReqText.text = appleReqAmt.ToString("F0");
        pollenReqText.text = pollenReqAmt.ToString("F0");

        if (inTree)
        {
            UIstuff.SetActive(true);
        }
        else
        {
            UIstuff.SetActive(false);
        }

        switch (state)
        {
            case State.NotSpawning:
                if (inTree)
                {
                    spriterenderer.sprite = not_smile_highlight;
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        if(GameManager.manager.apple >= appleReqAmt && GameManager.manager.pollen >= pollenReqAmt)//required amt to upgrade
                        {
                            lvl++;
                            mainTreeMaxHealth += 100;
                            GameManager.manager.apple -= appleReqAmt;
                            GameManager.manager.pollen -= pollenReqAmt;
                            GameManager.manager.Main_Tree_Health = mainTreeMaxHealth;
                            amtToSpawn += 2;
                            appleReqAmt += 2;
                            pollenReqAmt += 5;
                        }
                    }
                }
                else
                {
                    spriterenderer.sprite = not_smile_not_highlight;
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (inTree)
                    {
                        remainingSpawnAmt += amtToSpawn;
                        nextSpawnTimer = .1f;
                        state = State.Spawning;
                    }
                    

                }
                break;

            case State.Spawning:

                if (inTree)
                {
                    spriterenderer.sprite = smile_highlight;
                }
                else
                {
                    spriterenderer.sprite = smile_not_highlight;
                }


                if (remainingSpawnAmt > 0)
                {
                    nextSpawnTimer -= 1 * Time.deltaTime;
                    if (nextSpawnTimer < 0)
                    {
                        petalPool.SpawnPetal(transform, spawnforce);
                        nextSpawnTimer = spawnRate;
                        remainingSpawnAmt--;

                        if (remainingSpawnAmt <= 0)
                        {
                            state = State.NotSpawning;
                        }
                    }
                }
                break;
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
