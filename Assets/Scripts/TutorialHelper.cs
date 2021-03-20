using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tutorialObj;
    
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < tutorialObj.Length; i++)
        {
            if (GameManager.manager.tutorialOn)
            {
                tutorialObj[i].SetActive(true);
            }
            else
            {
                tutorialObj[i].SetActive(false);
            }
        }
    }
}
