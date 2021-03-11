using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class detecttest : MonoBehaviour
{
    [SerializeField]
    private Transform detectTransform;
    [SerializeField]
    private bool somethingInside;
    [SerializeField]
    private Vector2 boxSize;
    [SerializeField]
    private LayerMask whatIsEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        somethingInside = Physics2D.OverlapBox((Vector2)detectTransform.position, boxSize, 0, whatIsEnemy);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)detectTransform.position, boxSize);
    }
}
