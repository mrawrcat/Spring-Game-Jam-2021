using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAttack : MonoBehaviour
{
    private enum State
    {
        Idle,
        Attacking,
    }
    private State state;

    //not moving right now program in move to area when rabbits in area
    private bool faceR;
    private bool foundEnemy;
    [SerializeField]
    private LayerMask whatIsEnemy;
    [SerializeField]
    private Transform atkPos;
    [SerializeField]
    private Vector2 atkBoxSize;
    [SerializeField]
    private float atkRate;
    private float atkTimer;
    private float health;
    private Rigidbody2D rb2d;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        /*
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
        */
        atkTimer -= Time.deltaTime;

        foundEnemy = Physics2D.OverlapBox((Vector2)atkPos.position, atkBoxSize, 0, whatIsEnemy);
        Collider2D[] enemies = Physics2D.OverlapBoxAll((Vector2)atkPos.position, atkBoxSize, 0, whatIsEnemy);

        if (foundEnemy)
        {
            Debug.Log(enemies.Length);
            foreach(Collider2D enemy in enemies)
            {
                
                if(enemy.GetComponent<BunnyMove>() != null)
                {
                    if(atkTimer <= 0)
                    {
                        enemy.GetComponent<BunnyMove>().TakeDmg(10);
                        atkTimer = atkRate;
                        anim.SetTrigger("Attack");
                    }
                }
            }
        }
    }

    private void Attack()
    {
        if(atkTimer <= 0)
        {
            anim.SetTrigger("Attack");
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)atkPos.position, atkBoxSize);

    }
}
