using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightRobot : MonoBehaviour
{
    private enum State
    {
        Idle,
        Attacking,
    }
    private State state;


    private bool faceR;
    private bool inTree;
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

        atkTimer -= Time.deltaTime;

        foundEnemy = Physics2D.OverlapBox((Vector2)atkPos.position, atkBoxSize, 0, whatIsEnemy);
        Collider2D[] enemies = Physics2D.OverlapBoxAll((Vector2)atkPos.position, atkBoxSize, 0, whatIsEnemy);

        if (foundEnemy)
        {
            foreach(Collider2D enemy in enemies)
            {
                if(enemy.GetComponent<BunnyMove>() != null)
                {
                    enemy.GetComponent<BunnyMove>().TakeDmg(10);
                }
            }
        }
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
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)atkPos.position, atkBoxSize);

    }
}
