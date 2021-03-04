using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private Transform atkpos;
    private float moveInputX, moveInputY;

    public float Atk_Radius;
    public LayerMask whatIsEnemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");

        if (moveInputX == 1)
        {
            atkpos.position = new Vector3(transform.position.x + 1f, transform.position.y, 0);
        }
        if (moveInputX == -1)
        {
            atkpos.position = new Vector3(transform.position.x - 1f, transform.position.y, 0);
        }
        if (moveInputY == 1)
        {
            atkpos.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
        }
        if (moveInputY == -1)
        {
            atkpos.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
        }


    }

    public void Attack_Move()
    {

        Collider2D[] hit_Enemies = Physics2D.OverlapCircleAll(atkpos.position, Atk_Radius, whatIsEnemy);
        foreach (Collider2D enemy in hit_Enemies)
        {
            //shake.Shake();
            /*
            if (enemy.GetComponent<Barrel>() != null)
            {
                //GameManager.manager.smash_sfx.Play();
                if (!isFrozen)
                {
                    StartCoroutine(hit_pause(hit_pause_dur));
                }
                enemy.GetComponent<Barrel>().Take_Dmg();
            }
            if (enemy.GetComponent<EnemyProjectile>() != null)
            {
                //GameManager.manager.smash_sfx.Play();
                if (!isFrozen)
                {
                    StartCoroutine(hit_pause(hit_pause_dur));
                }
                Debug.Log("attacked enemy projectile");
                enemy.GetComponent<EnemyProjectile>().Atk_Projectile();
            }
            */

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)atkpos.position, Atk_Radius);
    }
}
