using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public GameObject pointer;
    public GameObject ball;
    public GameObject bat;

    public Vector3 random;

    public RaycastHit hitting;
    LayerMask mask;

    public LayerMask leftPitch;
    public LayerMask rightPitch;

    void Update()
    { 
    //{
    //    if (Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
    //        mask = leftPitch;
    //        RightBat();

    //    }
    //    else if (Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        mask = rightPitch;
    //        leftBat();
    //    }
    //    ThrowingBallAtPoint();

        //ThrowingBallAtPoint();

        //PathOfthorwnBall();

        //if (GameObject.FindGameObjectWithTag("Player") == null)
        //{
        //    Invoke(nameof(PathOfthorwnBall), 1f);
        //}
    }
}

//    public void PathOfthorwnBall()
//    {
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        //Debug.Log(ray);

//        if (Physics.Raycast(ray, out hitting, 60f, mask))
//        {
            
//        }
//    }
//}
