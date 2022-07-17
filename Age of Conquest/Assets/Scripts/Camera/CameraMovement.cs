using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform cameraTransform;

    public float cameraSpeed;
    public float cameraTime;

    public float normalSpeed;
    public float fastSpeed;
    public float rotationAmount;
    public float scrollSpeed;

    public Vector3 newPosition;
    public Vector3 zoomAmount;
    public Vector3 newZoom;
    public Quaternion newRotation;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraSpeed = fastSpeed;
        }
        else
        {
            cameraSpeed = normalSpeed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * cameraSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -cameraSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -cameraSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * cameraSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }

        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        newZoom += new Vector3(zoomAmount.x, (zoomAmount.y * scrollSpeed) * Input.mouseScrollDelta.y, (zoomAmount.z * scrollSpeed) * Input.mouseScrollDelta.y);

        transform.position = Vector3.Lerp(transform.position, newPosition, (Time.deltaTime * cameraTime));
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, (Time.deltaTime * cameraTime));
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, (Time.deltaTime * cameraTime));
    }
}
