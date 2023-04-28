using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableObjects : MonoBehaviour
{
    [SerializeField] GameObject unstableTableAnchor, unstableChairAnchor, unstableLampAnchor;
    // Start is called before the first frame update
    void Start()
    {
        unstableTableAnchor.SetActive(false);
        unstableChairAnchor.SetActive(false);
        unstableLampAnchor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraManager.instance.unstableTable)
        {
            unstableTableAnchor.SetActive(true);
            unstableChairAnchor.SetActive(false);
            unstableLampAnchor.SetActive(false);
        }
        if (CameraManager.instance.unstableChair)
        {
            unstableTableAnchor.SetActive(false);
            unstableChairAnchor.SetActive(true);
            unstableLampAnchor.SetActive(false);
        }
        if (CameraManager.instance.unstableLamp)
        {
            unstableTableAnchor.SetActive(false);
            unstableLampAnchor.SetActive(true);
            unstableChairAnchor.SetActive(false);
        }
    }
}
