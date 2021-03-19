using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightAppleTree : MonoBehaviour
{
    private enum State
    {
        NotSpawning,
        Spawning,
    }
    private State state;
    [SerializeField]
    private Sprite highlight, not_highlight;
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
    private float dewReqAmt;
    private float pollenReqAmt;
    private int lvl;
    [SerializeField]
    private Text lvlTxt;
    [SerializeField]
    private Text dewReqText;
    [SerializeField]
    private Text pollenReqText;
    [SerializeField]
    private GameObject UIstuff;
    // Start is called before the first frame update
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        lvl = 1;
        amtToSpawn = 2;
        dewReqAmt = 10;
        pollenReqAmt = 1;
    }

    // Update is called once per frame
    void Update()
    {
        lvlTxt.text = lvl.ToString();
        dewReqText.text = dewReqAmt.ToString("F0");
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
                    spriterenderer.sprite = highlight;
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (GameManager.manager.dewdrop >= dewReqAmt && GameManager.manager.pollen >= pollenReqAmt)//required amt to upgrade
                        {
                            lvl++;
                            
                            GameManager.manager.dewdrop -= dewReqAmt;
                            GameManager.manager.pollen -= pollenReqAmt;
                            amtToSpawn += 2;
                            dewReqAmt += 10;
                            pollenReqAmt += 1;
                        }
                    }
                }
                else
                {
                    spriterenderer.sprite = not_highlight;
                }

                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
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
                    spriterenderer.sprite = highlight;
                }
                else
                {
                    spriterenderer.sprite = not_highlight;
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
