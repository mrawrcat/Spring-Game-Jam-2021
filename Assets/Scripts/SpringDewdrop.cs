using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringDewdrop : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            GameManager.manager.Spring_Dewdrop++;
            gameObject.SetActive(false);
        }
    }
}
