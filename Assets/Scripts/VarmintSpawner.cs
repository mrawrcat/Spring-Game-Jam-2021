using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VarmintSpawner : MonoBehaviour
{
    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    private State state;
    [SerializeField]
    private Text waveTxt, tilNextWaveTxt;
    [SerializeField]
    private Text gameOverWaveTxt;
    public int waveNumber;
    [SerializeField]
    private float timeTilSpawn; //time in between wave spawn
    private float spawnTime;
    private VarmintPool varmintPool;

    private float nextEnemySpawnTimer;
    private int remainingSpawnAmt;
    [SerializeField]
    private int amtToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        state = State.WaitingToSpawnNextWave;
        varmintPool = GetComponent<VarmintPool>();
        spawnTime = timeTilSpawn;
        waveNumber = 0;
        //amtToSpawn = 1;
    }

    // Update is called once per frame
    void Update()
    {
        waveTxt.text = "Wave: " + waveNumber.ToString("F0");
        tilNextWaveTxt.text = "Next Wave: " + spawnTime.ToString("F2");
        gameOverWaveTxt.text = "You finished at Wave: " + waveNumber.ToString("F0");
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                if (!GameManager.manager.dead)
                {
                    spawnTime -= Time.deltaTime;
                }
                if (spawnTime <= 0)
                {
                    SpawnWave();
                }
                break;

            case State.SpawningWave:
                if (remainingSpawnAmt > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0)
                    {
                        varmintPool.SpawnVarmint(transform);
                        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);
                        
                        nextEnemySpawnTimer = Random.Range(.1f, .5f);
                        remainingSpawnAmt--;

                        if(remainingSpawnAmt <= 0)
                        {
                            state = State.WaitingToSpawnNextWave;
                        }
                    }
                }
                break;
        }


        

        
    }

    private void SpawnWave()
    {
        //varmintPool.SpawnVarmint(transform);
        spawnTime = timeTilSpawn;
        remainingSpawnAmt = amtToSpawn;
        state = State.SpawningWave;
        waveNumber++;
        amtToSpawn = 1 + (3 * waveNumber);
        //transform.position = new Vector3(0, 0, 0);
    }
}
