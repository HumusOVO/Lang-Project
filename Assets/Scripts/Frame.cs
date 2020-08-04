using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : SerializedMonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //做碰撞抖动效果
        FindObjectOfType<CameraController>().Shock();
    }
}
