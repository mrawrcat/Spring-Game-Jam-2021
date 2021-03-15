using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    private void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        state = State.NotSpawning;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (state)
        {
            case State.NotSpawning:
                if (inTree)
                {
                    spriterenderer.sprite = not_smile_highlight;
                }
                else
                {
                    spriterenderer.sprite = not_smile_not_highlight;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    remainingSpawnAmt += amtToSpawn;
                    nextSpawnTimer = .1f;
                    state = State.Spawning;

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
