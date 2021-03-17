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
    [SerializeField]
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
    private Rigidbody2D rb2d;
    private Animator anim;
    private RobotHealth robohealth;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        robohealth = GetComponentInChildren<RobotHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //robot originally faces left -> face right is -1
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
        
        atkTimer -= Time.deltaTime;

        foundEnemy = Physics2D.OverlapBox((Vector2)atkPos.position, atkBoxSize, 0, whatIsEnemy);
        Collider2D[] enemies = Physics2D.OverlapBoxAll((Vector2)atkPos.position, atkBoxSize, 0, whatIsEnemy);

        if (foundEnemy)
        {
            Debug.Log(enemies.Length);
            if (atkTimer <= 0)
            {
                if(robohealth.health > 0)
                {
                    foreach (Collider2D enemy in enemies)
                    {
                        if (enemy.GetComponent<BunnyMove>() != null)
                        {
                            enemy.GetComponent<BunnyMove>().TakeDmg(1);
                        }
                    }
                    anim.SetTrigger("Attack");
                    atkTimer = atkRate;
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

    public void DecreaseAtkRate()
    {
        if(atkRate > .1f)
        {
            atkRate -= .05f;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)atkPos.position, atkBoxSize);

    }
}
