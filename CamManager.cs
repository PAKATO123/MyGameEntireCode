using UnityEngine;

public class CamManager : MonoBehaviour
{ public bool Working;
    public GameObject CAM;
    private void Start()
    {
       Working = true;
        CAM.transform.position = new Vector3(0, 0, -10);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && Working) { CAM.SetActive(true); }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && Working) { CAM.SetActive(false); }
    }
}
