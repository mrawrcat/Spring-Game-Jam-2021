using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Varmint : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private Transform treeTransform;
    [SerializeField]
    private float speed = 1f;
    private bool faceR = false;
    private float health = 100;
    [Header("Obstacle Detection")]
    [SerializeField]
    private Transform detection;
    [SerializeField]
    private bool detectedObstacle = false;
    [SerializeField]
    private LayerMask whatIsObstacle;
    

    [Header("Attack")]
    [SerializeField]
    private float atkRate;
    private float atkTimer;
    [SerializeField]
    private GameObject detectedObstacleObj;
   
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        treeTransform = FindObjectOfType<MainTreeBuyDrop>().transform;
    }

    // Update is called once per frame 
    void Update()
    {
        Vector3 scaler = transform.localScale;
        if (faceR)
        {
            scaler.x = 1;
            transform.localScale = scaler;
        }
        else
        {
            scaler.x = -1;
            transform.localScale = scaler;
        }

        if(transform.position.x < treeTransform.position.x && !detectedObstacle)
        {
            //move right
            faceR = false;
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
        else if(transform.position.x > treeTransform.position.x && !detectedObstacle)
        {
            //move left
            faceR = true;
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }

        //Detect_Obstacle();
        if(atkTimer >= 0 && detectedObstacle)
        {
            atkTimer -=  1* Time.deltaTime;
        }

        
        if (atkTimer <= 0 && detectedObstacle)
        {
            if(detectedObstacleObj.gameObject.GetComponent<Obstacle>() != null)
            {
                detectedObstacleObj.gameObject.GetComponent<Obstacle>().Take_Dmg(5);
            }
            else if(detectedObstacleObj.gameObject.GetComponent<MainTreeBuyDrop>() != null)
            {
                detectedObstacleObj.gameObject.GetComponent<MainTreeBuyDrop>().Main_Tree_Take_Dmg(5);
            }
            atkTimer = atkRate;
            Attack();
            //anim.ResetTrigger("Attack");
        }
        
        
    }

    
    
    private void Attack()
    {
        anim.SetTrigger("Attack");
    }

    private void Take_Dmg(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            //play death animation then setactive false
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Obstacle")
        {
            detectedObstacle = true;
            detectedObstacleObj = collision.gameObject;
        }
        if(collision.tag == "Main Tree")
        {
            detectedObstacle = true;
            detectedObstacleObj = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            detectedObstacle = true;
            detectedObstacleObj = collision.gameObject;
        }
        if (collision.tag == "Main Tree")
        {
            detectedObstacle = true;
            detectedObstacleObj = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            detectedObstacle = false;
            detectedObstacleObj = null;
        }
        if (collision.tag == "Main Tree")
        {
            detectedObstacle = true;
            detectedObstacleObj = collision.gameObject;
        }
    }

    

}
