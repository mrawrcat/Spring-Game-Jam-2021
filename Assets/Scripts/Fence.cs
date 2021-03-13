using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{

    public float health;
    private BoxCollider2D fence2d;
    // Start is called before the first frame update
    void Start()
    {
        fence2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            fence2d.enabled = false;
        }
        else
        {
            fence2d.enabled = true;
        }
    }

    public void TakeDmg(float amt)
    {
        health -= amt;
    }
}
