using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public float zoomSpeed = 20;
    float cameraDistance = 10f;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)

            transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);
        {
            cameraDistance += scroll * zoomSpeed;
            cameraDistance *= scroll * zoomSpeed;
        }

    }
}
