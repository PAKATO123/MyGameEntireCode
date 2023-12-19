using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public GameObject Player;
    public Animator LevelTransAnim;
    public float transtime,wintime;
    public AudioClip SwipeSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger) { LevelChange(); }
    }
    public void LevelChange() { StartCoroutine(leveltrans(SceneManager.GetActiveScene().buildIndex + 1)); }
    IEnumerator leveltrans(int lvlindex) 
    {
        Player.transform.position = gameObject.transform.position;
        Player.GetComponent<Input_recive>().WinState();
        yield return new WaitForSeconds(wintime);
        LevelTransAnim.SetTrigger("ChangeScene");
        yield return new WaitForSeconds(transtime/2);
        GameObject.Find("MultiSceneSoundManager").GetComponent<AudioSource>().PlayOneShot(SwipeSound,0.2f);
        yield return new WaitForSeconds(transtime / 2);
        SceneManager.LoadScene(lvlindex);
    }
}
