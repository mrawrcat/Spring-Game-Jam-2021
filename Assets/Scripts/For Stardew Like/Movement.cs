using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float moveInputX, moveInputY;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInputX = Input.GetAxis("Horizontal");
        moveInputY = Input.GetAxis("Vertical");

        rb2d.velocity = new Vector2(moveInputX * speed, moveInputY * speed);
        //transform.position = transform.position + new Vector3(moveInputX * speed * Time.deltaTime, moveInputY * speed * Time.deltaTime, 0);
    }
}
