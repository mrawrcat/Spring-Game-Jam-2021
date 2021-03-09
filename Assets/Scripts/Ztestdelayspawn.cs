using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ztestdelayspawn : MonoBehaviour
{
    [SerializeField]
    private ObjectPoolNS applePool;
    [SerializeField]
    private float spawnforce;
    [SerializeField]
    private float spawnAmt;
    // Start is called before the first frame update
    void Start()
    {
        //spawnforce = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //do delay spawn
            StartCoroutine(SpawnDelay(spawnAmt));
        }
    }

    private IEnumerator SpawnDelay(float amt)
    {
        for (int i = 0; i < amt; i++)
        {
            applePool.SpawnCoin(transform, spawnforce);
            yield return new WaitForSeconds(.3f);
        }
        
    }
}
