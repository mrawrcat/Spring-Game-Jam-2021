using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DewdropBuyDrop : MonoBehaviour
{
    [SerializeField]
    private Slider dewdrop_Progress;
    private float progress;
    private bool pressingDown;
    [SerializeField]
    private float dropActionRate;
    private bool inDewdrop;

    [SerializeField]
    private ObjectPoolNS dewdropPool;
    [SerializeField]
    private Transform dropPoint1;
    // Start is called before the first frame update
    void Start()
    {
        dewdrop_Progress.minValue = 0;
        dewdrop_Progress.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        dewdrop_Progress.value = progress;
        pressingDown = Input.GetKey(KeyCode.E);


        if (progress == 100)
        {
            dewdropPool.SpawnCoin(dropPoint1);
            progress = 0;
            dropActionRate = 1;

        }

        if (inDewdrop)
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
    private void FixedUpdate()
    {
        while (dropActionRate > 0)
        {
            dropActionRate -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inDewdrop = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inDewdrop = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inDewdrop = false;
            progress = 0;
        }
    }
}
