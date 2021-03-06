using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Take_Dmg(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
