using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    #region simgle

    public static GameManager instance;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = null;
    }

    #endregion

    
}
