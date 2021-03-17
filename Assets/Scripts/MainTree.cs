using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainTree : MonoBehaviour
{

    public void TakeDmg(float dmg)
    {
        GameManager.manager.Main_Tree_Health -= dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            if(collision.gameObject.GetComponent<BunnyMove>() != null)
            {
                collision.gameObject.GetComponent<BunnyMove>().TakeDmg(1);
            }
        }
    }
}
