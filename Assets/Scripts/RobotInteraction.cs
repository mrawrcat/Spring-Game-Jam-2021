using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject UIstuff;
    private bool inRobot;
    private Animator anim;
    private bool faceR;
    [SerializeField]
    private RobotHealth robohealth;
    [SerializeField]
    private RobotAttack roboAttack;
    public float appleReqAmt;
    public float pollenReqAmt;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        
        faceR = false;
        appleReqAmt = 10;
        pollenReqAmt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scaler = transform.localScale;
        if (faceR)
        {
            scaler.x = -1;
            transform.localScale = scaler;
        }
        else
        {
            scaler.x = 1;
            transform.localScale = scaler;
        }
        //get local scale of robot -> if robot scale = -1 this scale = -1
        anim.SetBool("inRobot", inRobot);

        if (inRobot)
        {
            UIstuff.SetActive(true);
            if (!robohealth.enemyInside)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (GameManager.manager.apple >= appleReqAmt && GameManager.manager.pollen >= pollenReqAmt)
                    {
                        robohealth.IncreaseLvl();
                        roboAttack.DecreaseAtkRate();
                        GameManager.manager.apple -= appleReqAmt;
                        GameManager.manager.pollen -= pollenReqAmt;
                        appleReqAmt += 10;
                        pollenReqAmt += 2;
                    }
                }
            }
            
            
        }
        else
        {
            UIstuff.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRobot = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRobot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRobot = false;
        }
    }
}
