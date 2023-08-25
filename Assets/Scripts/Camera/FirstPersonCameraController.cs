using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonCameraController : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 100f;
    //public Image crosshairImage;
    //float xAxisClamp = 0.0f;
    private float rotationX;
    private float rotationY;

    public float minAngle;
    public float maxAngle;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //for mouse Control
    void Update()
    {        
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationY = Mathf.Clamp(rotationY, minAngle, maxAngle);
        rotationX = Mathf.Clamp(rotationX, -15f, 20f);

        //transform.rotation = Quaternion.Euler(rotationX,rotationY, 0);
        playerBody.rotation = Quaternion.Euler(rotationX, rotationY, 0);      
    }

    private void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.rotation = rotation;
        //UpdateCrosshairPosition();sa
    }

    //private void UpdateCrosshairPosition()
    //{
    //    if (crosshairImage != null)
    //    {
    //        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
    //        crosshairImage.rectTransform.position = screenCenter;
    //    }
    //}
}


