using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
    public float horizontalOffset, verticalOffset, minHorizontalViewDistance = 15f, maxHorizontalViewDistance = 25f,
        minVerticalViewDistance = 7f, maxVerticalViewDistance = 10f, cameraSmooth = 1;
    float leftBorder, rightBorder, bottomBorder, topBorder, distance;

    Vector3 positionOffset;
    Transform blindGuy;
    Camera myCamera;

	void Start () 
    {
        blindGuy = GameObject.FindWithTag("Blindguy").GetComponent<Transform>();
        myCamera = GetComponent<Camera>();
    }

    public void AdjustOffsetValues(Vector2 offSetValues)
    {
        horizontalOffset += offSetValues.x;
        verticalOffset += offSetValues.y;
    }

    public void CalculateCameraWorldValues()
    {
        transform.position = blindGuy.position + new Vector3(0, 0, transform.position.z);

        distance = myCamera.transform.position.z;

        leftBorder = myCamera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x;
        rightBorder = myCamera.ViewportToWorldPoint(new Vector3(1, 0, distance)).x;

        bottomBorder = myCamera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y;
        topBorder = myCamera.ViewportToWorldPoint(new Vector3(0, 1, distance)).y;
    }

    public void SetVerticalCameraSize()
    {
        if (maxVerticalViewDistance <= 0 || minVerticalViewDistance <= 0)
            return;
        while (topBorder - bottomBorder < maxVerticalViewDistance)
        {
            myCamera.orthographicSize += 0.1f;
            CalculateCameraWorldValues();
        }
        while (topBorder - bottomBorder > minVerticalViewDistance)
        {
            Camera.main.orthographicSize -= 0.1f;
            CalculateCameraWorldValues();
        }
    }

    public void SetHorizontalCameraSize()
    {
        if (maxHorizontalViewDistance <= 0 || minHorizontalViewDistance <= 0)
            return;
        while (rightBorder - leftBorder < minHorizontalViewDistance)
        {
            myCamera.orthographicSize += 0.1f;
            CalculateCameraWorldValues();
        }
        while (rightBorder - leftBorder > maxHorizontalViewDistance)
        {
            Camera.main.orthographicSize -= 0.1f;
            CalculateCameraWorldValues();
        }
    }

    public void SetCameraPosition()
    {
        positionOffset = blindGuy.position - new Vector3(leftBorder + horizontalOffset, bottomBorder + verticalOffset, 0);
        positionOffset.z = distance;
        transform.position = blindGuy.position + positionOffset;
    }

    public void HandleCameraMovement()
    {
        Vector3 cameraVelocity = ((blindGuy.position + positionOffset) - transform.position) * cameraSmooth;
        transform.position += cameraVelocity * Time.deltaTime;
    }
}
