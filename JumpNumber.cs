using UnityEngine;

public class JumpNumber : MonoBehaviour
{
    public Animator postnumber;
    void numberchange()
    { postnumber.SetTrigger("change"); }
}
