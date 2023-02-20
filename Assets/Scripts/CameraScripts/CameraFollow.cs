using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    //private Vector3 velocity = Vector3.zero;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float zoomSpeed = 2f;
    public float minZoom = 10f;
    public float maxZoom = 50f;
    private Camera cameraZoom;

    private float originalOrthographicSize;
    private bool isZoomingIn = false;
    private float currentZoomInTime = 0f;
    public float zoomInDuration = 0.5f;
    public bool isAttacked;

    void Start()
    {
        cameraZoom = GetComponent<Camera>();
        originalOrthographicSize = cameraZoom.orthographicSize;
    }

    void Update()
    {
        
        Vector3 desiredPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
        if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
        if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }

        if (isAttacked)
        {
            if (!isZoomingIn)
            {
                currentZoomInTime = 0f;
                isZoomingIn = true;
            }
            else
            {
                currentZoomInTime += Time.deltaTime;
                cameraZoom.orthographicSize = Mathf.Lerp(originalOrthographicSize, minZoom, currentZoomInTime / zoomInDuration);

                if (currentZoomInTime >= zoomInDuration)
                {
                    isZoomingIn = false;
                    isAttacked = false;
                }
            }
        }
        else
        {
            cameraZoom.orthographicSize = Mathf.Clamp(cameraZoom.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, minZoom, maxZoom);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        cameraZoom.orthographicSize -= scroll * zoomSpeed;

        cameraZoom.orthographicSize = Mathf.Clamp(cameraZoom.orthographicSize, minZoom, maxZoom);
    }
}
