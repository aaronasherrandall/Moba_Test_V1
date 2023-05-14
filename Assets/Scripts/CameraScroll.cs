using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public Camera cam;
    private float camFOV = 70;
    public float zoomSpeed;

    private float mouseScrollInput;

	private void Awake()
	{
        cam = Camera.main;
	}

	void Start()
    {
        camFOV = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");

        camFOV -= mouseScrollInput * zoomSpeed;
        camFOV = Mathf.Clamp(camFOV, 30, 70);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camFOV, zoomSpeed);
    }
}
