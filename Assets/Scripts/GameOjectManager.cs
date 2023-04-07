using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOjectManager : MonoBehaviour
{
    public new List<GameObject> _listeObj = new List<GameObject>();

    #region SINGLETON
    public static GameOjectManager instance { get; private set; }
    #endregion

    #region AWAKE
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente

        instance = this;
    }
    #endregion
}
