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
    private Transform atkPos;
    [SerializeField]
    private bool detectedObstacle = false;
    [SerializeField]
    private LayerMask whatIsObstacle;
    [SerializeField]
    private Vector2 boxSize;
  
    [Header("Attack")]
    [SerializeField]
    private float atkRate;
    private float atkTimer;
   
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
        DetectObstacle();
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

        if(atkTimer >= 0 && detectedObstacle)
        {
            atkTimer -=  1* Time.deltaTime;
        }

        
        
        
        
    }

    private void DetectObstacle()
    {
        detectedObstacle = Physics2D.OverlapBox((Vector2)atkPos.position, boxSize, 0, whatIsObstacle);
        Collider2D obstacle = Physics2D.OverlapBox((Vector2)atkPos.position, boxSize, 0, whatIsObstacle);
        if (atkTimer <= 0 && detectedObstacle)
        {
            if(obstacle.GetComponent<Fence>() != null)
            {
                obstacle.GetComponent<Fence>().TakeDmg(1);
            }
            else if(obstacle.GetComponent<MainTreeBuyDrop>() != null)
            {
                obstacle.GetComponent<MainTreeBuyDrop>().Main_Tree_Take_Dmg(1);
            }
            atkTimer = atkRate;
            Attack();
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

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)atkPos.position, boxSize);
        
    }

}
