using UnityEngine;

public class VoidKillBox : MonoBehaviour
{
    public Animator PlayerAnimation;
    public void Start()
    {
        PlayerAnimation = GameObject.Find("PlayerAnimationBox").GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") {other.gameObject.GetComponent<CheckpointSystem>().Death(); PlayerAnimation.GetComponent<Animator>().SetTrigger("VoidAnim"); }
    }
}
