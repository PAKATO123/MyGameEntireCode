using System.Threading.Tasks;
using UnityEngine;

public class HiTrigger : MonoBehaviour
{
    public GameObject Pic,Locationpointer;
    public int TimeDif;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger) { Move(); }
    }
    public async void Move() { await Task.Delay(TimeDif); Pic.transform.position = Locationpointer.transform.position; }
}
