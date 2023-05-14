using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    [SerializeField] GameObject plank1Anchor, plank2Anchor, plank3Anchor, plank4Anchor;
    // Start is called before the first frame update
    void Start()
    {
        plank1Anchor.SetActive(false);
        plank2Anchor.SetActive(false);
        plank3Anchor.SetActive(false);
        plank4Anchor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraManager.instance.plank1)
        {

            plank1Anchor.SetActive(true);
            plank2Anchor.SetActive(false);
            plank3Anchor.SetActive(false);
            plank4Anchor.SetActive(false);
        }
        if (CameraManager.instance.plank2)
        {

            plank1Anchor.SetActive(false);
            plank2Anchor.SetActive(true);
            plank3Anchor.SetActive(false);
            plank4Anchor.SetActive(false);
        }
        if (CameraManager.instance.plank3)
        {

            plank1Anchor.SetActive(false);
            plank2Anchor.SetActive(false);
            plank3Anchor.SetActive(true);
            plank4Anchor.SetActive(false);
        }

        if (CameraManager.instance.plank4)
        {

            plank1Anchor.SetActive(false);
            plank2Anchor.SetActive(false);
            plank3Anchor.SetActive(false);
            plank4Anchor.SetActive(true);
        }

    }
}
