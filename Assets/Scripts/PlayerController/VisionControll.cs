using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VisionControll : MonoBehaviour
{
    public float mouseSensitiveX = 25f;
    public float mouseSensitiveY = 50;
    public Transform playerBody;
    float verticalRotation = 0f;

    void Awake()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {

    }

    float mouseInputx = 0f, mouseInputY = 0f;

    void FixedUpdate()
    {
        mouseInputx = (2 * Input.GetAxis("Mouse X") * mouseSensitiveX * Time.deltaTime + mouseInputx) / 3;
        mouseInputY = (2 * Input.GetAxis("Mouse Y") * mouseSensitiveY * Time.deltaTime + mouseInputY) / 3;
        //Debug.Log(mouseInputx);
        verticalRotation -= mouseInputY;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseInputx);
    }
}
