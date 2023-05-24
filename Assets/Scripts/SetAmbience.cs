using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class SetAmbience : MonoBehaviour
{
    /*[SerializeField] private AK.Wwise.RTPC positionRTPC;
    
    

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
    }*/

    [SerializeField] private AK.Wwise.Event houseAmbEvent;
    [SerializeField] private AK.Wwise.Event districtAmbEvent;
    [SerializeField] private AK.Wwise.RTPC distAmbRTPC;
    [SerializeField] private GameObject ambObj;

    private bool hasCollided = false;
    private bool isInHouse = false;
    private float value = 0.49f;
    private float t = 0f;

    private void Update()
    {
        if (hasCollided == true)
        {
            if (!isInHouse)
            {
                value = 1f;
                hasCollided = false;
                isInHouse = true;
            }
            else if (isInHouse) 
            {
                value = 0.49f;
                hasCollided = false;
                isInHouse = false;
            }
        }

        Debug.Log(value);
        distAmbRTPC.SetGlobalValue(value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            houseAmbEvent.Post(ambObj);
            hasCollided = true;
        }
    }

    public void stopDistrictAmbience() 
    {
        StartCoroutine(waitAndStopAmb());
    }

    private IEnumerator waitAndStopAmb()
    {
        yield return new WaitForSeconds(0.2f);
        districtAmbEvent.Stop(ambObj);
    }
}
