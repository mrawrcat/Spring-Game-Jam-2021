using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrackeysFade : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeSwitch(sceneName));
    }

    private IEnumerator FadeSwitch(string lvlname)
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(lvlname);

    }
}
