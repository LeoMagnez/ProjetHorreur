using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Animator credits;
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CreditsCoro()
    {
        StartCoroutine(WaitBeforeCredits());
    }

    public IEnumerator WaitBeforeCredits()
    {
        yield return new WaitForSeconds(50f);
        credits.SetTrigger("Credits");
    }
}
