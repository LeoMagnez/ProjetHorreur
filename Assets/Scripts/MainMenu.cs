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
        startOfGameCanvas.SetActive(false);
        yield return new WaitForSeconds(1f);
        startOfGameAnimator.SetTrigger("StartOfGame");

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("VS_District");

    }
}
