using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    [System.Serializable]
    public class ResourceType
    {
        public string resourceName;
        public int resourceAmt;
    }

    public List<ResourceType> resourceType;
    public Dictionary<string, Queue<ResourceType>> resourceDictionary;
    // Start is called before the first frame update
    void Start()
    {
        resourceDictionary = new Dictionary<string, Queue<ResourceType>>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void AddResource(string resourceName, int resourceAmt)
    {
        
    }
}
