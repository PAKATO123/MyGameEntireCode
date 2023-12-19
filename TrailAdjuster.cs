using UnityEngine;

public class TrailAdjuster : MonoBehaviour
{
    public GameObject SizeController;
    void FixedUpdate()
    {
        gameObject.GetComponent<TrailRenderer>().widthMultiplier = SizeController.transform.localScale.x;
    }
}
