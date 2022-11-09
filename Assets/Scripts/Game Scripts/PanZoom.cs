using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float minimumZoomLevel = 1f;
    public float maximumZoomLevel = 8f;

    float startOrthoSize;
     float mapMinX, mapMaxX,mapMinY, mapMaxY;


    private void Awake()
    {
        startOrthoSize = Camera.main.orthographicSize;
    }


    // Update is called once per frame
    void Update()
    {
        float currOrthoSize = Camera.main.orthographicSize;

        ZoomAndPinchScreen();



    }


    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minimumZoomLevel, maximumZoomLevel);
    }


    void ZoomAndPinchScreen()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float differenceBetweenTouch = currentMagnitude - prevMagnitude;

            Zoom(differenceBetweenTouch * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
}
