using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private float moveInputY;
    [SerializeField]
    private float pressTime;
    [SerializeField]
    private float pressTimeNext;
    [SerializeField]
    private bool pressingDown;
    private bool inInteractable;
    [SerializeField]
    private Transform coinSpawnpoint;
    [SerializeField]
    private ObjectPoolNS objpool;


    // Start is called before the first frame update
    void Start()
    {
        pressTimeNext = 2;
    }

    // Update is called once per frame
    void Update()
    {
        moveInputY = Input.GetAxisRaw("Vertical");

        if(pressTime >= pressTimeNext)
        {
            pressTimeNext += 2;
            DoSomething();

        }


        if(moveInputY == -1 && inInteractable)
        {
            pressTime += 1 * Time.deltaTime;
            //pressingDown = true;
        }
        else
        {
            pressTime = 0;
            pressTimeNext = 2;
            //pressingDown = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            GameManager.manager.apple--;
            objpool.SpawnCoin(coinSpawnpoint);
        }
        
    }

    private void DoSomething()
    {
        Debug.Log("dewdrop moves to fill slot");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Main Tree")
        {
            inInteractable = true;
            if (pressingDown)
            {
                //Debug.Log("interacting with tree");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Main Tree")
        {
            inInteractable = false;
        }
    }
}
