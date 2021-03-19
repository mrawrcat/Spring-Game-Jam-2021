using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainTree : MonoBehaviour
{

    public void TakeDmg(float dmg)
    {
        GameManager.manager.Main_Tree_Health -= dmg;
    }

    
}
