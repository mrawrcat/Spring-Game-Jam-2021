using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float moveInputX;
    
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool faceR = true;
    [SerializeField]
    private float speed;

    [Header("Collision Detection")]
    public Vector2 bottomOffset;
    public Vector2 boxSize;
    private bool isGrounded;
    [SerializeField]
    private LayerMask whatIsGround;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInputX = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, boxSize, 0, whatIsGround);
        Facing();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(moveInputX * speed, rb2d.velocity.y);
        setAnims();
    }

    private void setAnims()
    {
        anim.SetFloat("MoveInputX", Mathf.Abs(moveInputX));
        anim.SetBool("isGrounded", isGrounded);
    }

    private void Flip()
    {
        faceR = !faceR;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    private void Facing()
    {
        if (faceR && moveInputX < 0)
        {
            Flip();
        }
        else if (!faceR && moveInputX > 0)
        {
            Flip();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, boxSize);
    }
}
