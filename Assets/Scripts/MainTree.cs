using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTree : MonoBehaviour, IInteractable
{
    public string interactionID { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        interactionID = "Main Tree";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {

    }
}
