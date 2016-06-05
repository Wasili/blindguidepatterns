using UnityEngine;
using System.Collections;

public class BlindGuyCameraHandler : MonoBehaviour {
    CameraBehaviour cameraBehaviour;

	void Start ()
    {
        cameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();
        Invoke("SetCameraStartValues", 0.01f);
	}


    void SetCameraStartValues()
    {
        cameraBehaviour.AdjustOffsetValues((Vector2)GetComponent<Renderer>().bounds.size * 0.5f);
        cameraBehaviour.CalculateCameraWorldValues();
        cameraBehaviour.SetVerticalCameraSize();
        cameraBehaviour.SetHorizontalCameraSize();
        cameraBehaviour.SetCameraPosition();
    }

    void Update()
    {
        if (cameraBehaviour == null)
            cameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();
        cameraBehaviour.HandleCameraMovement();
	}
}
