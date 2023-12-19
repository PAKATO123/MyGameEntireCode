using System.Collections;
using UnityEngine;

public class Selfdes : MonoBehaviour
{
    public float Lifespan;
    void Start()
    {
        StartCoroutine(Selfdrestruct());
    }
    IEnumerator Selfdrestruct()
    { yield return new WaitForSeconds(Lifespan);
        Destroy(gameObject);
    }
}
