using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float boltSpeed = 5;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        rb.velocity = transform.forward * boltSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "BoltSwitch")
        {
            Debug.Log("Charged");
            collision.collider.GetComponent<Charger>().Activate();
        }
        Destroy(gameObject);
    }

}
