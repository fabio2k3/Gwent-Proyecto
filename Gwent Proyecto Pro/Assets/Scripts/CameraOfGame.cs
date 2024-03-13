using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOfGame : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if(target != null)
        {
            Vector3 directionToTarget = target.position - transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

            transform.rotation = lookRotation;
        }
    }
}
