using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : SerializedMonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //����ײ����Ч��
        FindObjectOfType<CameraController>().Shock();
    }
}
