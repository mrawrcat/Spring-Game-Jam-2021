using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petal : MonoBehaviour
{
    private Transform objectParent;
    private ResourceManager resourceManager;
    private void Start()
    {
        objectParent = GameObject.Find("Petal Pool").transform;
        //resourceManager = FindObjectOfType<ResourceManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameManager.manager.dewdrop++;
            
            
            gameObject.SetActive(false);
            transform.parent = objectParent;

        }

        if (collision.collider.tag == "Platform")
        {
            transform.parent = collision.transform;
        }
    }
}
