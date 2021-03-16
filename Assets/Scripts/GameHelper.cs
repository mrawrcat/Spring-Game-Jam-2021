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
    [SerializeField]
    private Text pollenText;
    

    // Update is called once per frame
    void Update()
    {
        dewdropText.text = GameManager.manager.dewdrop.ToString("F0");
        appleText.text = GameManager.manager.apple.ToString("F0");
        pollenText.text = GameManager.manager.pollen.ToString("F0");
    }
}
