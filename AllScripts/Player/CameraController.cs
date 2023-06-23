using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField]
    private Transform cameraPos;

    private float startFov, targetFov;
    private float zoomSpeed = 10f;

    public Camera cam;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        startFov = cam.fieldOfView;
        targetFov = cam.fieldOfView;    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = cameraPos.position;
        transform.rotation = cameraPos.rotation;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFov, zoomSpeed * Time.deltaTime);
    }

    public void ZoomIn(float zoom)
    {
        targetFov = zoom;
    }

    public void ZoomOut()
    {
        targetFov = startFov;
    }
}
