using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChanger : MonoBehaviour
{
    [SerializeField] int angle = 45;

    public void RotateDirection()
    {
        transform.forward = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bolt")
        {
            Vector3 newPos = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);

            other.transform.position = newPos;
            other.transform.forward = transform.forward;
        }
    }

}
