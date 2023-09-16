using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltShooter : MonoBehaviour
{
    [SerializeField] Transform BoltPrefab;
    [SerializeField] float spawnTime = 4;
    [SerializeField] Transform spawnpoint;


    //private void Start()
    //{
    //    InvokeRepeating(nameof(ShootBolt), spawnTime, spawnTime);
    //}

    public void ShootBolt()
    {
        var bolt = Instantiate(BoltPrefab, spawnpoint.position, Quaternion.identity);
        bolt.forward = transform.forward;
    }


}
