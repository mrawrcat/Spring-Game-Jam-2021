using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTreeBuyDrop : MonoBehaviour
{

    [SerializeField]
    private Slider mainTree_Progress;
    private bool pressingDown;
    [SerializeField]
    private float dropActionRate;
    private float progress;
    private bool inTree;
    [SerializeField]
    private ObjectPoolNS applePool;
    [SerializeField]
    private Transform dropPoint1;
    // Start is called before the first frame update
    void Start()
    {
        mainTree_Progress.minValue = 0;
        mainTree_Progress.maxValue = 100;

    }

    // Update is called once per frame
    void Update()
    {
        pressingDown = Input.GetKey(KeyCode.S);
        mainTree_Progress.value = progress;

        while(dropActionRate > 0)
        {
            dropActionRate -= Time.deltaTime;
        }

        if(progress == 100)
        {
            applePool.SpawnCoin(dropPoint1);
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


            if (Input.GetKeyUp(KeyCode.S))
            {
                progress = 0;
                //dropActionRate = 1;
            }
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
            Debug.Log("left tree");
        }
    }

}
