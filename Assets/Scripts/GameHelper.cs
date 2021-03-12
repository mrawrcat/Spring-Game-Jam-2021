using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHelper : MonoBehaviour
{
    [SerializeField]
    private Text dewdropText;
    [SerializeField]
    private Text appleText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dewdropText.text = GameManager.manager.Spring_Dewdrop.ToString("F0");
        appleText.text = GameManager.manager.apple.ToString("F0");
    }
}
