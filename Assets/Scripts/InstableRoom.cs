using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstableRoom : MonoBehaviour
{
    [SerializeField] int instableCounter;

    public GameObject trigger_scene;

    public Animator slidingWall;
    public ParticleSystem firstOpening, slidingLeft, slidingRight;
    

    public void DecreaseCounter()
    {
        instableCounter--;

        if(instableCounter == 2)
        {
            StartCoroutine(Opening(true));
            slidingWall.SetTrigger("FirstObjectPlaced");
        }

        if(instableCounter == 1)
        {
            StartCoroutine(Opening(false));
            slidingWall.SetTrigger("SecondObjectPlaced");
        }

        if(instableCounter == 0)
        {
            StartCoroutine(Opening(false));
            slidingWall.SetTrigger("ThirdObjectPlaced");
            trigger_scene.SetActive(true);

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
