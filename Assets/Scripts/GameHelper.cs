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
    [SerializeField]
    private GameObject gameOverPanel;
    

    // Update is called once per frame
    void Update()
    {
        dewdropText.text = GameManager.manager.dewdrop.ToString("F0");
        appleText.text = GameManager.manager.apple.ToString("F0");
        pollenText.text = GameManager.manager.pollen.ToString("F0");

        if (GameManager.manager.dead)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void ResetDead()
    {
        GameManager.manager.Main_Tree_Health = 100;
        GameManager.manager.dead = false;
        GameManager.manager.dewdrop = 0;
        GameManager.manager.apple = 0;
        GameManager.manager.pollen = 0;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
