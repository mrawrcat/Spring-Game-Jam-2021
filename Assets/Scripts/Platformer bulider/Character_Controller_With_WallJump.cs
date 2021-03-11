using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller_With_WallJump : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float slideSpeed;
    private float moveInputX;
    private float moveInputY;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer sr;
    private bool faceR = true;

    private bool canMove;
    private bool wallGrab;
    [SerializeField]
    private bool wallJumped;
    //private bool wallSlide;
    private float wallJumpLerp;
    private Vector2 dir;
    private float groundcounter;
    [SerializeField]
    private float groundtime; //how much coyote time
    private bool groundTouch;

    [Header("WallJumps")]
    [SerializeField]
    private Vector2 WallJumpLeft;
    [SerializeField]
    private Vector2 WallJumpRight;

    [Header("Collision Detection")]
    public Vector2 bottomOffset;
    public Vector2 boxSize;
    public Vector2 lOffset;
    public Vector2 lBoxSize;
    public Vector2 rOffset;
    public Vector2 rBoxSize;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool onWall;
    private bool onLeftWall;
    private bool onRightWall;
    private int wallSide;
    [SerializeField]
    private LayerMask whatIsGround;


    // Start is called before the first frame update
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        moveInputX = Input.GetAxis("Horizontal");
        moveInputY = Input.GetAxis("Vertical");
        dir = new Vector2(moveInputX, moveInputY);
        setAnims();
        isGrounded = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, boxSize, 0, whatIsGround);
        Facing();
        onWall = Physics2D.OverlapBox((Vector2)transform.position + rOffset, rBoxSize, 0, whatIsGround) || Physics2D.OverlapBox((Vector2)transform.position + lOffset, lBoxSize, 0, whatIsGround);
        onLeftWall = Physics2D.OverlapBox((Vector2)transform.position + lOffset, lBoxSize, 0, whatIsGround);
        onRightWall = Physics2D.OverlapBox((Vector2)transform.position + rOffset, rBoxSize, 0, whatIsGround);

        if (onRightWall)
        {
            wallSide = 1;
        }
        if (onLeftWall)
        {
            wallSide = -1;
        }

        if (isGrounded && !groundTouch)
        {
            //wallSlide = false;
            wallJumped = false;
            groundTouch = true;
            //TouchGround();
        }
        else if(isGrounded && groundTouch)
        {

            transform.parent = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, boxSize, 0, whatIsGround).gameObject.transform;
            //Debug.Log(Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, boxSize, 0, whatIsGround).name);
        }
        else
        {
            transform.parent = null;
        }


        Walk(dir);
        WallSlide();

        JumpCheck();
    }

    private void FixedUpdate()
    {

        //rb2d.velocity = new Vector2(moveInputX * speed, rb2d.velocity.y);
    }


    private void Walk(Vector2 dir)
    {
        if (!wallJumped)
        {
            rb2d.velocity = new Vector2(dir.x * speed, rb2d.velocity.y);
        }
        else if (wallJumped)
        {
            rb2d.velocity = Vector2.Lerp(rb2d.velocity, new Vector2(dir.x * speed, rb2d.velocity.y), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir)
    {
        rb2d.velocity = dir * jumpForce;
    }

    private void JumpCheck()
    {
        if (isGrounded)
        {
            wallJumped = false;
            groundcounter = groundtime;
        }
        else if (!isGrounded)
        {
            groundcounter -= Time.deltaTime;
            groundTouch = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (groundcounter > 0)
            {
                //anim.SetTrigger("jump");
                Jump(Vector2.up);
                groundcounter = -1;
                
            }
            if (onWall && !isGrounded)
            {
                WallJump();
            }

        }
    }

    private void WallJump()
    {
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.01f));
        if (wallSide == -1)
        {
            rb2d.velocity = WallJumpLeft + Vector2.left;
            Debug.Log("WallJumped");
            wallJumped = true;

        }
        if (wallSide == 1)
        {
            rb2d.velocity = WallJumpRight + Vector2.right;
            Debug.Log("WallJumped");
            wallJumped = true;
        }

    }
    private void WallSlide()
    {

        if (onWall && !isGrounded && rb2d.velocity.y < 0)
        {

            if (rb2d.velocity.y < -slideSpeed)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -slideSpeed);
            }
            //wallSlide = true;
        }
    }

    private void setAnims()
    {
        anim.SetFloat("MoveInputX", Mathf.Abs(moveInputX));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("onWall", onWall);
        anim.SetFloat("VerticalVelocity", rb2d.velocity.y);
    }
    private void SpriteFlip(int side)
    {
        bool state = (side == 1) ? false : true;
        sr.flipX = state;
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
    public void ClearForces()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.tag == "Platform")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Platform")
        {
            transform.parent = null;
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, boxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + lOffset, lBoxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + rOffset, rBoxSize);
    }
}
