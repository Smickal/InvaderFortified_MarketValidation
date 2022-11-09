using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Camera mainCam;

    Vector3 startingMousePos;
    [SerializeField]float minZoom = 1;
    [SerializeField]float maxZoom = 8;

    [SerializeField] SpriteRenderer sprite;

    float mapMinX, mapMaxX,mapMinY, mapMaxY;
    Vector3 difference;
    private void Awake() 
    {
        mapMinX = sprite.transform.position.x - sprite.bounds.size.x / 2f;
        mapMaxX = sprite.transform.position.x + sprite.bounds.size.x / 2f;
        mapMinY = sprite.transform.position.y - sprite.bounds.size.y /2f;
        mapMaxY = sprite.transform.position.y + sprite.bounds.size.y / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startingMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
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

            ZoomCamera(-differenceBetweenTouch * 0.01f);
        }
        else if(Input.GetMouseButton(0))
        {
            difference = startingMousePos - mainCam.ScreenToWorldPoint(Input.mousePosition);
            mainCam.transform.position = ClampCamera(difference + mainCam.transform.position);
        }
        ZoomCamera(-Input.GetAxis("Mouse ScrollWheel"));
        
     }

    void ZoomCamera(float numbers)
    {
        float zoomScaler = numbers;
        mainCam.orthographicSize = Mathf.Clamp(mainCam.orthographicSize + zoomScaler, minZoom,maxZoom);
        mainCam.transform.position = ClampCamera(mainCam.transform.position);
        
    }

    Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = mainCam.orthographicSize;
        float camWidth = camHeight *mainCam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x , minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX,newY, targetPosition.z);
    }
}
