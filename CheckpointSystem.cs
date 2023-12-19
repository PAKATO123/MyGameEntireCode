using System.Threading.Tasks;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{ public GameObject Player,Reloader, DieExo,PlayerAnimation,TimePauser;
    public Rigidbody2D PlayerRigid;
    public Transform CurrentCP=null;
    public TrailRenderer WhiteTrail;
    public bool Reloadroom = false;
    public AudioClip DeadSound, ReviveSound,ReloadSound;
    public AudioSource Asource;
    public void Start()
    {
        Asource = gameObject.GetComponent<AudioSource>();
        if (ISthereTimeController()) { TimePauser = GameObject.Find("TimeController"); }
       

    }
    bool ISthereTimeController() { return GameObject.Find("TimeController").GetComponent<TimeController>(); }
    private void OnTriggerEnter2D(Collider2D collision)
    {    
        if (collision.CompareTag("ResZone")) { CurrentCP = collision.GetComponent<CheckpointManager>().SP; }
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E)) { Deathmove(); }
    }
    async void Deathmove() { gameObject.transform.position = CurrentCP.position;await Task.Delay(10); WhiteTrail.emitting = true; Player.GetComponent<Input_recive>().Alive = true; }
    public async void Death() {
        TimePauser.GetComponent<TimeController>().RespawnTime();
        Player.GetComponent<Input_recive>().SpeedCapX = 0;
        PlayerRigid.constraints = RigidbodyConstraints2D.FreezePosition;
        await Task.Delay(200);
        Player.GetComponent<Input_recive>().Alive = false;
        Asource.PlayOneShot(DeadSound, 0.2F);
        Respawn();
    }
    
    public async void Respawn()
    {
        Reloader.GetComponent<Animator>().SetTrigger("Reload");
        await Task.Delay(500);
        Asource.PlayOneShot(ReloadSound, 0.2f);
        await Task.Delay(500);
        Reloadroom = true;
        PlayerAnimation.GetComponent<Animator>().SetTrigger("Revive");
        gameObject.transform.position = CurrentCP.position;
        WhiteTrail.Clear();
        Reloader.GetComponent<Animator>().SetTrigger("ReloadEx");
        await Task.Delay(10);
        Reloadroom = false;
        await Task.Delay(500);
        Asource.PlayOneShot(ReviveSound,0.2f);
        await Task.Delay(650);
        PlayerRigid.constraints = RigidbodyConstraints2D.None;
        Reloader.GetComponent<Animator>().SetTrigger("ReloadReset");
        Player.GetComponent<Input_recive>().Alive = true;

        TimePauser.GetComponent<TimeController>().ReturntoPretime();
        PlayerAnimation.GetComponent<Animator>().ResetTrigger("Revive");
    }

}
