using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraScale : MonoBehaviour
{
    public Camera m_camera;
    public float targetHeight = 540f;
    public float unitsPerWidth = 40f;

    private float pixelsToUnits;

    private void Awake()
    {
        SetCameraScale();
    }

    public void SetCameraScale()
    {
        var height = Mathf.RoundToInt(targetHeight / (float) Screen.width * Screen.height);
        pixelsToUnits = targetHeight / unitsPerWidth;
        m_camera.orthographicSize = height / pixelsToUnits / 2;
    }
}