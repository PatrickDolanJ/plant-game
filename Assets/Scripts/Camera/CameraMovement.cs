using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Tooltip("Refernece to taget object camera will follow")]
    public GameObject target;
    [Tooltip("Distance between targer and camera at which the camrea will stop trying to get closer")]
    public float minDistance = 2.0f;
    [Tooltip("Min speed camera will move at, important for SmoothStep")]
    public float minSpeed = 2.0f;
    [Tooltip("Max speed camera will move at")]
    public float maxSpeed = 20.0f;
    [Tooltip("Used to help bring down speed")]
    public float smoothFactor = 0.125f;

    void Update()
    {
        if (target != null)
        {
            Vector3 playerPos = target.transform.position;
            Vector3 camPos = this.transform.position;
            //Hand calulating to ingore y component
            float distance = Mathf.Sqrt(Mathf.Pow((camPos.x - playerPos.x) + Mathf.Exp(camPos.z - playerPos.z), 2));
            if (distance > minDistance)
            {
                float speed = Mathf.SmoothStep(maxSpeed, minSpeed, distance / 10.0f);
                Vector3 newPosition = Vector3.Lerp(this.gameObject.transform.position, target.transform.position, speed * smoothFactor * Time.deltaTime);
                this.gameObject.transform.position = new Vector3(newPosition.x, this.gameObject.transform.position.y, newPosition.z);
            }
        }
    }

    // Maybe public method to set target in case we want the
    // gameManager to have the camera focus on somethign else temporarily
    public void setTarget(GameObject target)
    {
        this.target = target;
    }
}