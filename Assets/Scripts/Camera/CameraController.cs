using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform CameraPOS;
    //public float offsetY;
    //public float offsetZ;
   
    private void Start()
    {
        //transform.position = new Vector3 (CameraPOS.position.x, CameraPOS.position.y + offsetY, CameraPOS.position.z+ offsetZ);
        transform.position = CameraPOS.position;

        //transform.rotation = CameraPOS.rotation;

    }
}
