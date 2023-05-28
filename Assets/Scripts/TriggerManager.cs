using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TriggerManager : MonoBehaviour
{

    public int _TriggerToCall = 0;
    public string _SceneToLoad;
    public Image imageTuto;
    public string TextTuto;

    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;
    public UnityEvent TriggerStay;
    private bool endfoHasStarted = false;
    private bool sdBandaid = false;

    [SerializeField] private GameObject phone;
    [SerializeField] private AK.Wwise.Event ringSFX;
    [SerializeField] private AK.Wwise.Event slamSFX;
    [SerializeField] private GameObject blackSquareDoorEffect;

    [SerializeField] private GameObject sfxObj3;
    [SerializeField] private GameObject ambObj3;
    [SerializeField] private AK.Wwise.Event lv3Amb;
    [SerializeField] private AK.Wwise.Event flipNLSFX;
    [SerializeField] private AK.Wwise.Event flipLSFX;

    [SerializeField] private GameObject door;

    private float count = 0.0f;

    public Animator SceneFadeOut;

    public int indexCanvas;
    public List<GameObject> CanvasList = new List<GameObject>();

    private void Start()
    {
        AkSoundEngine.SetRTPCValue("distAmount", 0.0f);
    }

    private void Update()
    {
        if (endfoHasStarted)
        {
            count += 2.1f * Time.deltaTime;
            AkSoundEngine.SetRTPCValue("distAmount", count);
            sdBandaid = true;
        }
        else if (!endfoHasStarted && sdBandaid) 
        {
            StartCoroutine(waitForStopSd());
        }

        if (count > 100f && endfoHasStarted)
        {
            //FIN DU JEU LOCK DES MOUVEMENTS <- IMPORTANT
            blackSquareDoorEffect.SetActive(true);
            lv3Amb.Stop(ambObj3);
            flipNLSFX.Post(sfxObj3);
            endfoHasStarted = false;
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

    private void OnTriggerStay(Collider other)
    {
        TriggerStay.Invoke();
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
        //text.SetText("" + TextTuto);
        switch (indexCanvas)
        {
            case 0:
                CanvasList[indexCanvas].SetActive(true);
                yield return new WaitForSeconds(3f);
                CanvasList[indexCanvas].SetActive(false);
                break;
            case 1:
                CanvasList[indexCanvas].SetActive(true);
                yield return new WaitForSeconds(3f);
                CanvasList[indexCanvas].SetActive(false);
                break;

            case 2:
                CanvasList[indexCanvas].SetActive(true);
                yield return new WaitForSeconds(3f);
                CanvasList[indexCanvas].SetActive(false);
                break;

            case 3:
                CanvasList[indexCanvas].SetActive(true);
                yield return new WaitForSeconds(3f);
                CanvasList[indexCanvas].SetActive(false);
                break;
        }

    }

    private IEnumerator waitForStopSd() 
    {
        yield return new WaitForSeconds(1f);
        flipLSFX.Stop(sfxObj3);
        flipNLSFX.Stop(sfxObj3);
    }

    public IEnumerator setTutoImages()
    {
        yield return new WaitForSeconds(3f);
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
        flipNLSFX.Post(sfxObj3);
        yield return new WaitForSeconds(0.5f);
        blackSquareDoorEffect.SetActive(false);
        flipLSFX.Post(sfxObj3);
    }
}
