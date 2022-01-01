using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFaceCamera : MonoBehaviour
{
    public Transform cameraTransform;

    private void Start()
    {
        if (cameraTransform == null)
            cameraTransform = GameObject.Find("UI Camera").transform; //TODO:
    }

    void Update()
    {
        transform.rotation = cameraTransform.rotation;
    }
}
