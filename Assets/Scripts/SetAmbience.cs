using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

public class SetAmbience : MonoBehaviour
{
    /*[SerializeField] private AK.Wwise.RTPC positionRTPC;
    private bool hasCollided = false;
    private float value = 1f;
    private float t = 0f;

    private void Awake()
    {
        positionRTPC.SetGlobalValue(0.61f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            hasCollided = true;
        }
    }

    private void Update()
    {
        if (hasCollided == true) 
        {
            Debug.Log(value);
            value = Mathf.Lerp(0.61f, 0f, t*2);
            t += 0.1f * Time.deltaTime;
            positionRTPC.SetGlobalValue(value);
        }
    }*/

    [SerializeField] private AK.Wwise.Event houseAmbEvent;
    [SerializeField] private AK.Wwise.Event districtAmbEvent;
    [SerializeField] private GameObject ambObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            houseAmbEvent.Post(ambObj);
        }
    }

    public void stopDistrictAmbience() 
    {
        StartCoroutine(waitAndStopAmb());
    }

    private IEnumerator waitAndStopAmb()
    {
        yield return new WaitForSeconds(0.3f);
        districtAmbEvent.Stop(ambObj);
    }
}
