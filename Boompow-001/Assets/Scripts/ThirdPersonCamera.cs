﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
//
     float cameraDistanceMax = 5f;
     float cameraDistanceMin = 1f;
     float cameraDistance = 2f;
     float scrollSpeed = -1f;
//
    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;


    float yaw;
    float pitch;

    void Start() {
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }

	void Update ()
    {
        dstFromTarget += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        dstFromTarget = Mathf.Clamp(dstFromTarget, cameraDistanceMin, cameraDistanceMax);
    }

	void LateUpdate () {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * dstFromTarget;
       


        // set camera position



    }
}
