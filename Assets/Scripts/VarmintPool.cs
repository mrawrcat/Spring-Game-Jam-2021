using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarmintPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public Transform Grid;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    [Header("Spawner tags")]
    public string[] tags;
    private void Start()
    {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(Grid.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);

            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }


    public GameObject SpawnVarmint(string tag, Transform pos)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with Tag " + tag + " doesnt exist");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        //objToSpawn.transform.SetAsFirstSibling();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos.position;
        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }

    public void SpawnVarmint(Transform pos)
    {
        SpawnVarmint(tags[0], pos);
    }
}
