using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShapeFillController : MonoBehaviour
{
    public RectTransform startPoint;
    public RectTransform endPoint;
    public Image fillImage;
    public GameObject startCollider;

    internal bool isfilled;
    internal Action filled;

    private Camera mainCamera;
    private bool isDrawing = false;
    private Vector2 drawingStartPos;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        //getting input from mouse
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = 10f;
            Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
            if (hit != null && hit.gameObject == startCollider)
            {
                isDrawing = true;
                drawingStartPos = mouseWorldPos;
            }
        }

        if (Input.GetMouseButton(0) && isDrawing)
        {
            UpdateFill();
        }

        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            isDrawing = false;

            if (fillImage.fillAmount < 1f)
            {
                fillImage.fillAmount = 0f; // Reset if not complete
            }
        }
    }

    void UpdateFill()
    {
        Vector2 startScreen = RectTransformUtility.WorldToScreenPoint(mainCamera, startPoint.position);
        Vector2 endScreen = RectTransformUtility.WorldToScreenPoint(mainCamera, endPoint.position);
        Vector2 currentMouse = Input.mousePosition;

        //Debug.Log("start pos: " + startScreen);
        //Debug.Log("end pos: " + endScreen);
        //Debug.Log("mouse pos: " + currentMouse);

        Vector2 direction = endScreen - startScreen;
        float totalDistance = direction.magnitude;
        direction.Normalize();

        float projected = Vector2.Dot(currentMouse - startScreen, direction);
        float clampedDistance = Mathf.Clamp(projected, 0f, totalDistance);
        float progress = clampedDistance / totalDistance;

        fillImage.fillAmount = progress;
        if(fillImage.fillAmount == 1 && !isfilled)
        {
            startCollider.SetActive(false);
            isfilled = true;
            //Debug.Log(isfilled);
            filled.Invoke();
        }
    }
}
