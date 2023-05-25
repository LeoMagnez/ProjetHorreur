using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TriggerManager : MonoBehaviour
{

    public int _TriggerToCall = 0;
    public string _SceneToLoad;
    public TMP_Text text;
    public string TextTuto;

    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;
    private bool endfoHasStarted = false;

    [SerializeField] private GameObject phone;
    [SerializeField] private AK.Wwise.Event ringSFX;
    [SerializeField] private AK.Wwise.Event slamSFX;
    [SerializeField] private GameObject blackSquareDoorEffect;

    [SerializeField] private GameObject sfxObj;
    [SerializeField] private AK.Wwise.Event flipNLSFX;
    [SerializeField] private AK.Wwise.Event flipLSFX;
    [SerializeField] private AK.Wwise.RTPC distAmount;
    private float count = 0.0f;

    public Animator SceneFadeOut;

    private void Update()
    {
        if (endfoHasStarted) 
        {
            count += 2.38f * Time.deltaTime;
            distAmount.SetGlobalValue(count);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        //TriggerEnter.Invoke();
        TriggerExit.Invoke();
    }

    public void destroyTrigger()
    {
        Destroy(gameObject);
    }

    public void changeLevel()
    {
        //SceneManager.LoadScene(_SceneToLoad);
        SceneFadeOut.SetTrigger("FadeOut");
        StartCoroutine(ChangeSceneCoroutine());
    }

    public void CoroutineFinal()
    {
        SceneFadeOut.SetTrigger("FadeOutFinal");
        endfoHasStarted = true;
    }

    public void playRingingSFX()
    {
        ringSFX.Post(phone);
    }

    public void stopRingingSFX()
    {
        ringSFX.Stop(phone);
        slamSFX.Post(phone);
    }

    public void triggerTuto()
    {
        CameraManager.instance.cameraTuto = true;
    }

    public void CoroutineTest()
    {
            StartCoroutine(bookSelect());
    }

    public IEnumerator bookSelect()
    {
        yield return new WaitForSeconds(3f);
        TriggerExit.Invoke();
    }

    public void SetText()
    {
        StartCoroutine(TextTime());
    }

    public void WaitForSuicide()
    {
        StartCoroutine(TimeBeforeSuicide());
    }

    public void FinalSceneDoorEffect()
    {
        StartCoroutine(FinalScene());
    }

    public IEnumerator TextTime()
    {
        text.SetText("" + TextTuto);
        yield return new WaitForSeconds(3f);
        text.SetText("");
    }

    public IEnumerator TimeBeforeSuicide()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(_SceneToLoad);
    }

    public IEnumerator FinalScene()
    {
        blackSquareDoorEffect.SetActive(true);
        flipNLSFX.Post(sfxObj);
        yield return new WaitForSeconds(0.5f);
        blackSquareDoorEffect.SetActive(false);
        flipLSFX.Post(sfxObj);
    }
}
