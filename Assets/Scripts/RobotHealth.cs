using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotHealth : MonoBehaviour
{
    [SerializeField]
    private Slider healthbar;
    [SerializeField]
    private Text lvlTxt;
    [SerializeField]
    private Text appleReqText;
    [SerializeField]
    private Text pollenReqText;
    private float maxHealth;
    public float health;
    private int lvl;
    [SerializeField]
    private RobotInteraction robotInteract;
    private BoxCollider2D collider2d;
    public bool enemyInside;
    [SerializeField]
    private LayerMask whatIsEnemy;
    [SerializeField]
    private Vector2 offSet;
    [SerializeField]
    private Vector2 boxSize;
    // Start is called before the first frame update
    void Start()
    {
        lvl = 1;
        health = 100;
        maxHealth = 100;
        healthbar.minValue = 0;
        collider2d = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        lvlTxt.text = "Lvl: " + lvl.ToString();
        appleReqText.text = robotInteract.appleReqAmt.ToString("F0");
        pollenReqText.text = robotInteract.pollenReqAmt.ToString("F0");
        healthbar.maxValue = maxHealth;
        healthbar.value = health;
        enemyInside = Physics2D.OverlapBox((Vector2)transform.position + offSet, boxSize, 0, whatIsEnemy);
        if (health <= 0)
        {
            collider2d.enabled = false;
        }
        else
        {
            collider2d.enabled = true;
        }


    }

    public void IncreaseLvl()
    {
        lvl++;
        maxHealth += 20;
        health = maxHealth;
    }
    public void TakeDmg(float dmg)
    {
        health -= dmg;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + offSet, boxSize);

    }
}
