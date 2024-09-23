using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatDisplay : MonoBehaviour
{
    public float refreshRate = 0.5f; // Update every 0.5 seconds
    private float playerX;
    private float playerZ;
    private float timer;
    private int frames;
    private float fps;
    public GameObject player;

    void Update()
    {
        frames++;
        timer += Time.unscaledDeltaTime;

        if (timer >= refreshRate)
        {
            fps = frames / timer;
            frames = 0;
            timer = 0f;
        }
        playerX = player.transform.position.x;
        playerZ = player.transform.position.z;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 20), "FPS: " + Mathf.RoundToInt(fps));
        GUI.Label(new Rect(10, 50, 150, 20), " x: " + playerX + " , y: " + playerZ);
    }
}
