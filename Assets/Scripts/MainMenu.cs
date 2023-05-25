using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject startOfGameCanvas;

    public Animator startOfGameAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame()
    {
        StartCoroutine(WaitForStartOfGame());
    }
    public IEnumerator WaitForStartOfGame()
    {
        startOfGameAnimator.SetTrigger("StartOfGame");

        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("VS_District");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
