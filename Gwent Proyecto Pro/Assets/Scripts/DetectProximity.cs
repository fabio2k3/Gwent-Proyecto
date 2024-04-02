using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectProximity : MonoBehaviour
{
    public GameObject messageText;
    public Transform cameraTransform;

    private bool collisionDetected = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TablePlayer1") && !collisionDetected)
        {
            Vector3 position = cameraTransform.position + cameraTransform.forward * 3f;
            messageText.transform.position = position;
            messageText.SetActive(true);
            collisionDetected = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("TablePlayer1"))
        {
            messageText.SetActive(false);
            collisionDetected = false;
        }
    }
}
