using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraScale : MonoBehaviour
{
    public Camera camera;
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
        camera.orthographicSize = height / pixelsToUnits / 2;
    }
}