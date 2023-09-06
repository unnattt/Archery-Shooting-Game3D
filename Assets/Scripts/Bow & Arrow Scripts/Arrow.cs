using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{    
    Rigidbody arrowRB;
    CapsuleCollider frontCollider;
    BoxCollider backCollider;
    void Start()
    {
        frontCollider = GetComponent<CapsuleCollider>();
        backCollider = GetComponent<BoxCollider>();
        arrowRB = GetComponent<Rigidbody>();
        arrowRB.isKinematic = true;
        frontCollider.enabled = false;
        backCollider.enabled = true;
    }

    public void Thrower(Vector3 force)
    {
        arrowRB.isKinematic = false;
        frontCollider.enabled = true;
        backCollider.enabled = false;
        arrowRB.AddForce(force,ForceMode.Impulse);
        //transform.rotation = Quaternion.LookRotation();
        //transform.SetParent(null);
        ArrorDestroyer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ArcheryBoard"))
        {
            Debug.Log("is Working");
            arrowRB.isKinematic = true;
        }
    }
    public void InstantDestroy()
    {
        Destroy(gameObject);
    }
    
    public void ArrorDestroyer()
    {
        Destroy(gameObject,10f);
    }
}
