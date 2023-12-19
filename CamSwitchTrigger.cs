using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitchTrigger : MonoBehaviour
{
    public GameObject PreCamParent,PostCam,PreCam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger) { PreCam.SetActive(false);PostCam.SetActive(true);PreCamParent.GetComponent<CamManager>().Working = false; }
    }
}
