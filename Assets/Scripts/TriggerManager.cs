using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TriggerManager : MonoBehaviour
{
    //public string monTrigger;

    //public List<String> _mesTrigger = new List<String>();
    public int _TriggerToCall = 0;
    public string _SceneToLoad;

    public UnityEvent CorridorEnter;
    public UnityEvent CorridorExit;

    private void OnTriggerEnter(Collider other)
    {
        CorridorEnter.Invoke();

        if (other.tag == "Player")
        {


            //call monTrigger;

            //_mesTrigger[_TriggerToCall];

            switch (_TriggerToCall)
            {
                case 0:
                    GameOjectManager.instance._listeObj[0].SetActive(false);
                    GameOjectManager.instance._listeObj[1].SetActive(true);
                    GameOjectManager.instance._listeObj[2].SetActive(false);
                    //GameOjectManager.instance._listeObj[3].SetActive(true);
                    GameOjectManager.instance._listeObj[4].SetActive(false);
                    GameOjectManager.instance._listeObj[5].SetActive(true);
                    GameOjectManager.instance._listeObj[6].SetActive(false);
                    GameOjectManager.instance._listeObj[7].SetActive(true);
                    GameOjectManager.instance._listeObj[8].SetActive(true);
                    GameOjectManager.instance._listeObj[9].SetActive(true);
                    gameObject.SetActive(false);
                    Debug.Log("0");
                    break;

                case 1:

                    GameOjectManager.instance._listeObj[11].SetActive(true);
                    gameObject.SetActive(false);
                    Debug.Log("1");
                    break;

                case 2:


                    GameOjectManager.instance._listeObj[13].SetActive(true);
                    gameObject.SetActive(false);
                    Debug.Log("2");
                    break;

                case 3:

                    SceneManager.LoadScene(_SceneToLoad);
                    gameObject.SetActive(false);
                    Debug.Log("3");
                    break;

                case 4:

                    //GameOjectManager.instance._listeObj[17];

                    Debug.Log("4");
                    break;


            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        CorridorExit.Invoke();
    }

    public void destroyTrigger()
    {
        Destroy(gameObject);
    }


}
