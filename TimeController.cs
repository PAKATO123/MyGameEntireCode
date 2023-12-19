using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool IsPaused = false;
    public float PreDeadTime = 1;

    void Update()
    {
        if (!IsPaused) {
            if (Input.GetKeyDown(KeyCode.DownArrow) && Time.timeScale != 0.25F) { Time.timeScale -= 0.25F; }
            if (Input.GetKeyDown(KeyCode.UpArrow) && Time.timeScale != 2F ) { Time.timeScale += 0.25F; } }
  
    }
    public void ReturntoPretime() { Time.timeScale = PreDeadTime; }
    public void RespawnTime() { PreDeadTime = Time.timeScale; if (Time.timeScale < 1) { Time.timeScale = 1; } }
}
