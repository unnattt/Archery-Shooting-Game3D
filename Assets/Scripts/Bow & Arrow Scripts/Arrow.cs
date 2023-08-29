using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] Transform tips;
    Rigidbody arrowRB;
    [HideInInspector]
    public bool isRelease;

    void Start()
    {
        arrowRB = GetComponent<Rigidbody>();
        arrowRB.isKinematic = true;
    }

    void Update()
    {
        if (isRelease)
            ArrorDestroyer();       
    }

    public void Thrower(Vector3 force)
    {
        arrowRB.isKinematic = false;
        arrowRB.AddForce(force, ForceMode.Impulse);
        //transform.rotation = Quaternion.LookRotation();
        transform.SetParent(null);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ArcheryBoard"))
        {
            Debug.Log("is Working");
            arrowRB.isKinematic = true;
        }
    }
       

    public void ArrorDestroyer()
    {
        Destroy(gameObject,10f);
    }
}
