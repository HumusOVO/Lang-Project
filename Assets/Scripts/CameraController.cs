using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void Shock()
    {
        transform.DOPunchPosition(new Vector3(0, 0.1f, 0), 1, 1, 0f);
        transform.position = new Vector3(0, 0, -10);
    }
}
