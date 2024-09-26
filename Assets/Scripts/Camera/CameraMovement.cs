using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public float minDistance = 2.0f;
    public float minSpeed = 2.0f;
    public float maxSpeed = 20.0f;
    public float smoothFactor = 0.125f;

    void Update()
    {
        if (target != null)
        {
            Vector3 playerPos = target.transform.position;
            Vector3 camPos = this.transform.position;
            //Hand calulating to ingore y component
            float distance = Mathf.Sqrt(Mathf.Pow((camPos.x - playerPos.x) + Mathf.Exp(camPos.z - playerPos.z),2));
            if (distance > minDistance)
            {
                float speed = Mathf.SmoothStep(maxSpeed, minSpeed, distance / 10.0f);
                Vector3 newPosition = Vector3.Lerp(this.gameObject.transform.position, target.transform.position, speed * smoothFactor * Time.deltaTime);
                this.gameObject.transform.position = new Vector3(newPosition.x, this.gameObject.transform.position.y, newPosition.z);
            }
        }
    }
}