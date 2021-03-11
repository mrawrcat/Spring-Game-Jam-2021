using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petal : MonoBehaviour
{
    private Transform objectParent;
    private void Start()
    {
        objectParent = GameObject.Find("Petal Pool").transform;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameManager.manager.Spring_Dewdrop++;
            gameObject.SetActive(false);
            transform.parent = objectParent;

        }

        if (collision.collider.tag == "Platform")
        {
            transform.parent = collision.transform;
        }
    }
}
