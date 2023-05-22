using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TriggerManager : MonoBehaviour
{

    public int _TriggerToCall = 0;
    public string _SceneToLoad;

    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;

    [SerializeField] private GameObject phone;
    [SerializeField] private AK.Wwise.Event ringSFX;
    [SerializeField] private AK.Wwise.Event slamSFX;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter.Invoke();

    }

    private void OnTriggerExit(Collider other)
    {
        TriggerEnter.Invoke();
    }

    public void destroyTrigger()
    {
        Destroy(gameObject);
    }

    public void changeLevel()
    {
        SceneManager.LoadScene(_SceneToLoad);
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
        Debug.Log("La coroutine marche");
    }

}
