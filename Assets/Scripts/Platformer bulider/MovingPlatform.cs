using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject platform;
    [SerializeField]
    private Transform[] platformTransform;

    private bool pressedO;
    // Start is called before the first frame update
    void Start()
    {
        platform.transform.position = platformTransform[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            pressedO = true;
        }

        if (pressedO)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, platformTransform[1].position, 1 * Time.deltaTime);
        }
    }
}
