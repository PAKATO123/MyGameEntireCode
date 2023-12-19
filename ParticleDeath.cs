using UnityEngine;

public class ParticleDeath : MonoBehaviour
{
    public GameObject Player, DieParticles,Winparticles;
    void ParticleSpawner() { Instantiate(DieParticles, Player.transform.position, Quaternion.identity); }
    void Winparticle() { Instantiate(Winparticles, Player.transform.position, Quaternion.identity); }
    
}
