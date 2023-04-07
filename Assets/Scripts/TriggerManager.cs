using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    //public string monTrigger;

    //public List<String> _mesTrigger = new List<String>();
    public int _TriggerToCall = 0;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //call monTrigger;

            //_mesTrigger[_TriggerToCall];

            switch (_TriggerToCall)
            {
                case 0:
                    GameOjectManager.instance._listeObj[0].SetActive(true);
                    GameOjectManager.instance._listeObj[1].SetActive(true);
                    GameOjectManager.instance._listeObj[2].SetActive(false);
                    GameOjectManager.instance._listeObj[3].SetActive(true);
                    GameOjectManager.instance._listeObj[4].SetActive(true);
                    GameOjectManager.instance._listeObj[5].SetActive(false);
                    GameOjectManager.instance._listeObj[6].SetActive(true);
                    GameOjectManager.instance._listeObj[7].SetActive(true);
                    GameOjectManager.instance._listeObj[8].SetActive(true);
                    Debug.Log("0");
                    break;

                case 1:
                    Debug.Log("1");
                    break;


            }

        }
    }



}
