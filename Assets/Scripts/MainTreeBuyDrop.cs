using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTreeBuyDrop : MonoBehaviour
{

    [SerializeField]
    private Slider mainTree_Progress;
    [SerializeField]
    private Text levelTxt;
    [SerializeField]
    private Text amtNeededTxt;
    private float amtNeeded;
    public float level;
    private bool pressingDown;
    [SerializeField]
    private float dropActionRate;
    private float progress;
    private bool inTree;

    [SerializeField]
    private float spawnforce;
    [SerializeField]
    private ObjectPoolNS applePool;
    [SerializeField]
    private Transform dropPoint1;
    // Start is called before the first frame update
    void Start()
    {
        mainTree_Progress.minValue = 0;
        mainTree_Progress.maxValue = 100;
        level = 1;
        amtNeeded = 3;
    }

    // Update is called once per frame
    void Update()
    {
        pressingDown = Input.GetKey(KeyCode.E);
        mainTree_Progress.value = progress;
        levelTxt.text = "LVL: " + level.ToString();
        amtNeededTxt.text = amtNeeded.ToString();


        if(GameManager.manager.Spring_Dewdrop >= amtNeeded)
        {
            if (progress == 100)
            {

                applePool.SpawnCoin(dropPoint1, spawnforce);
                GameManager.manager.Spring_Dewdrop -= amtNeeded;
                amtNeeded *= 2;
                level++;
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


                if (Input.GetKeyUp(KeyCode.E))
                {
                    progress = 0;
                    //dropActionRate = 1;
                }
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

    public void Main_Tree_Take_Dmg(float dmg)
    {
        GameManager.manager.Main_Tree_Health -= dmg;
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
            progress = 0;
        }
    }

}
