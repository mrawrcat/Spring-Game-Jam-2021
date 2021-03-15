using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ztestdelayspawn : MonoBehaviour
{

    private enum State
    {
        NotSpawning,
        Spawning,
    }
    [SerializeField]
    private State state;

    [SerializeField]
    private PetalPool petalPool;
    [SerializeField]
    private float spawnforce;
   

    
    private float nextSpawnTimer;
    private int remainingSpawnAmt;
    [SerializeField]
    private int amtToSpawn;
    [SerializeField]
    private float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        //spawnforce = 5;
        state = State.NotSpawning;
    }

    // Update is called once per frame
    void Update()
    {
        

        switch (state)
        {
            case State.NotSpawning:
                if (Input.GetKeyDown(KeyCode.R))
                {
                    remainingSpawnAmt += amtToSpawn;
                    nextSpawnTimer = .1f;
                    state = State.Spawning;
                    
                }
                break;

            case State.Spawning:
                if(remainingSpawnAmt > 0)
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        remainingSpawnAmt += amtToSpawn;
                    }

                    nextSpawnTimer -=  1 * Time.deltaTime;
                    if(nextSpawnTimer < 0)
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

    private IEnumerator SpawnDelay(float amt)
    {
        for (int i = 0; i < amt; i++)
        {
            petalPool.SpawnPetal(transform, spawnforce);
            yield return new WaitForSeconds(.3f);
        }
        
    }
}
