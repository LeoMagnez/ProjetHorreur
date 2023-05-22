using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstableRoom : MonoBehaviour
{
    [SerializeField] int instableCounter;

    public GameObject trigger_scene;

    public Animator slidingWall;
    public ParticleSystem firstOpening, slidingLeft, slidingRight;

    [Header("Sound")]
    [SerializeField] private AK.Wwise.Event wallSlide1;
    [SerializeField] private AK.Wwise.Event wallSlide2;
    [SerializeField] private GameObject sfxObj;

    public void DecreaseCounter()
    {
        instableCounter--;

        if(instableCounter == 2)
        {
            StartCoroutine(Opening(true));
            slidingWall.SetTrigger("FirstObjectPlaced");
            wallSlide1.Post(sfxObj);
        }

        if(instableCounter == 1)
        {
            StartCoroutine(Opening(false));
            slidingWall.SetTrigger("SecondObjectPlaced");
            wallSlide2.Post(sfxObj);
        }

        if(instableCounter == 0)
        {
            StartCoroutine(Opening(false));
            slidingWall.SetTrigger("ThirdObjectPlaced");
            trigger_scene.SetActive(true);
            wallSlide2.Post(sfxObj);
        }
    }

    public IEnumerator Opening(bool firstTime)
    {
        if (firstTime)
        {
            firstOpening.Play();
            yield return new WaitForSeconds(1f);
        }

        slidingLeft.Play();
        slidingRight.Play();
    }
   
}
