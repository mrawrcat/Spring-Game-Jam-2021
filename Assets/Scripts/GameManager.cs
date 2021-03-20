using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public float dewdrop;
    public float apple;
    public float pollen;
    public float Main_Tree_Health;
    public bool dead;
    public bool tutorialOn;
    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        tutorialOn = true;
    }

    private void Update()
    {
        if(Main_Tree_Health <= 0)
        {
            dead = true;
        }
        else
        {
            dead = false;
        }
    }
}
