using System.Threading.Tasks;
using UnityEngine;

public class WhyTrigger : MonoBehaviour

{
    public GameObject WhyPic;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger) { Why(); }
    }
    public async void Why() { await Task.Delay(1010);WhyPic.transform.position = new Vector3(48,8,0);  }
}
