using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMove : MonoBehaviour
{
    private bool isGrounded;
    private bool foundObstacle;
    private bool faceR;
    public float health;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsObstacle;
    [SerializeField]
    private Vector2 boxSize, offSet;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private Transform atkPos;
    [SerializeField]
    private Vector2 atkBoxSize;
    [SerializeField]
    private float atkRate;
    private float atkTimer;
    [SerializeField]
    private float atkPower;
    private Rigidbody2D rb2d;
    private Animator anim;
    private VarmintSpawner wavespawner;
    private Transform treeTransform;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        wavespawner = FindObjectOfType<VarmintSpawner>();
        health = 1;
        treeTransform = FindObjectOfType<MainTree>().transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            health = 0;
            gameObject.SetActive(false);
        }
        atkPower = 1 * wavespawner.waveNumber;
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
        anim.SetBool("isGrounded", isGrounded);
        atkTimer -= Time.deltaTime;
        isGrounded = Physics2D.OverlapBox((Vector2)transform.position + offSet, boxSize, 0, whatIsGround);
        foundObstacle = Physics2D.OverlapBox((Vector2)atkPos.position, atkBoxSize, 0, whatIsObstacle);
        Collider2D obstacle = Physics2D.OverlapBox((Vector2)atkPos.position, atkBoxSize, 0, whatIsObstacle);
        if (isGrounded && !foundObstacle)
        {
            if(transform.position.x < treeTransform.position.x)
            {
                faceR = true;
                Jump(direction);
            }
            else if(transform.position.x > treeTransform.position.x)
            {
                faceR = false;
                
                Jump(new Vector2(-direction.x, direction.y));
            }
        }
        else if (foundObstacle)
        {
            Attack();
            if (obstacle.GetComponent<Fence>() != null)
            {
                if(atkTimer < 0)
                {
                    obstacle.GetComponent<Fence>().TakeDmg(atkPower);
                    atkTimer = atkRate;
                }
            }
            else if(obstacle.GetComponent<MainTree>() != null)
            {
                if (atkTimer < 0)
                {
                    obstacle.GetComponent<MainTree>().TakeDmg(atkPower);
                    atkTimer = atkRate;
                }
                
            }
            else if(obstacle.GetComponent<RobotHealth>() != null)
            {
                if (atkTimer < 0)
                {
                    obstacle.GetComponent<RobotHealth>().TakeDmg(atkPower);
                    atkTimer = atkRate;
                }
                
            }
        }

        
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
    }

    private void Jump(Vector2 dir)
    {
        rb2d.velocity = dir * 1f;
        //rb2d.AddForce(dir, ForceMode2D.Force);
    }

    public void TakeDmg(float dmg)
    {
        health -= dmg;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + offSet, boxSize);
        Gizmos.DrawWireCube((Vector2)atkPos.position, atkBoxSize);

    }
}
