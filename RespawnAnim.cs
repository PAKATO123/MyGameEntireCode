using UnityEngine;

public class RespawnAnim : MonoBehaviour
{
    public GameObject ResAni, StartAni, Player;
    void RespawnParticles() { Instantiate(ResAni, Player.transform.position , Quaternion.identity);  }
    void StartParticles() { Instantiate(StartAni, Player.transform.position, Quaternion.identity); }

    void StartSound() { gameObject.GetComponentInParent<AudioSource>().PlayOneShot(gameObject.GetComponentInParent<CheckpointSystem>().ReviveSound,0.4f); }

}
