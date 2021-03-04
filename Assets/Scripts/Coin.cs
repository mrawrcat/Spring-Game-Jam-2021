using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float timer;
    private void Update()
    {
        timer += Time.deltaTime;
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(timer >= 2)
            {
                GameManager.manager.Spring_Dewdrop++;
                timer = 0;
                gameObject.SetActive(false);
            }
        }
    }
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (timer >= 0.5f)
            {
                GameManager.manager.Spring_Dewdrop++;
                timer = 0;
                gameObject.SetActive(false);
            }
        }
    }

}
