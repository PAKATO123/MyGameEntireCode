using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManagerFollow : MonoBehaviour
{
    public bool Working;
    public GameObject Player;
    public GameObject CAM;
    public float bufferDura;
    private void Start()
    {
        Working = true;
        CAM.GetComponent<CinemachineVirtualCamera>().m_Follow = null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && Working) { StartCoroutine(Cambuffer()); }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && Working) { CAM.SetActive(false); CAM.GetComponent<CinemachineVirtualCamera>().m_Follow = null; }
    }
    IEnumerator Cambuffer() { CAM.SetActive(true); yield return new WaitForSeconds(bufferDura); CAM.GetComponent<CinemachineVirtualCamera>().m_Follow = Player.transform; }
}
