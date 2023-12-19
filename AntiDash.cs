using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AntiDash : MonoBehaviour
{
    public bool InC;
    public GameObject AnimBox;
    public float WaitTime;
    Coroutine X;
    private Animator animator;

    public void Start()
    {animator = AnimBox.GetComponent<Animator>();}
    public void Dashed()
    {
        if (!InC) { animator.SetTrigger("Expand"); X = StartCoroutine(StayBig());InC = true; }
        else { RestartC(); }
    }


    IEnumerator StayBig() { yield return new WaitForSeconds(WaitTime); animator.SetTrigger("Shrink"); InC = false; }
    void RestartC() { StopCoroutine(X);X= StartCoroutine(StayBig()); }
}
