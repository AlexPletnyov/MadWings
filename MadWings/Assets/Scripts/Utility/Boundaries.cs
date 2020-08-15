using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public GameObject topBorder;
    public GameObject bottomBorder;
    public GameObject leftBorder;
    public GameObject rightBorder;
    
    private Vector2 screenBounds;

    private void Start()
    {
        SetScreenBounds();
    }

    public void SetScreenBounds()
    {
        screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        //позиционируем границы
        topBorder.transform.position = new Vector2(0, screenBounds.y);
        bottomBorder.transform.position = new Vector2(0, -screenBounds.y);
        leftBorder.transform.position = new Vector2(-screenBounds.x, 0);
        rightBorder.transform.position = new Vector2(screenBounds.x, 0);

        //высота и ширина границ с учётом текущей камеры
        var borderScaleWidth = new Vector2(screenBounds.x * 2, 0);
        var borderScaleHeight = new Vector2(0, screenBounds.y * 2);
        
        //задаем размеры границ
        topBorder.transform.localScale = borderScaleWidth;
        bottomBorder.transform.localScale = borderScaleWidth;
        leftBorder.transform.localScale = borderScaleHeight;
        rightBorder.transform.localScale = borderScaleHeight;
    }
}
