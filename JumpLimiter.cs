using UnityEngine;

public class JumpLimiter : MonoBehaviour
{
    public int JumpLimit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { collision.GetComponent<Input_recive>().JumpCount = JumpLimit; }
    }
}
