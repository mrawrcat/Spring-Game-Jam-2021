using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : MonoBehaviour
{
    private Transform objectParent;
    private void Start()
    {
        objectParent = GameObject.Find("Pollen Pool").transform;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameManager.manager.pollen++;
            gameObject.SetActive(false);
            transform.parent = objectParent;

        }

        if (collision.collider.tag == "Platform")
        {
            transform.parent = collision.transform;
        }
    }
   
    
}
