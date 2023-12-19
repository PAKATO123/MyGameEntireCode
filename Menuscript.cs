using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuscript : MonoBehaviour
{
    public float transtime = 1;
    public Animator LevelTransAnim;
    private bool ready = false;
    private int targetLevel;
    public AudioClip Swipesound;

    private void Start()
    {
        StartCoroutine(ReadyUp());
        targetLevel = PlayerPrefs.GetInt("savedlevel");
    }
    void Update()
    {   if (Input.GetKeyDown(KeyCode.Space) && ready && Input.GetKey(KeyCode.LeftShift)) { targetLevel = 1; LevelChange(); }
        else if (Input.GetKeyDown(KeyCode.Space) && ready) { LevelChange(); }
        if (Input.GetKeyDown(KeyCode.Escape)) { StartCoroutine(Exittrans()); }
        
    }
    public void LevelChange() { if (targetLevel == 0) { StartCoroutine(leveltrans(1)); } else { StartCoroutine(leveltrans(targetLevel)); } }
    IEnumerator leveltrans(int lvlindex)
    {
        LevelTransAnim.SetTrigger("ChangeScene");
        yield return new WaitForSeconds(transtime/2);
        GameObject.Find("MultiSceneSoundManager").GetComponent<AudioSource>().PlayOneShot(Swipesound, 0.2f);
        yield return new WaitForSeconds(transtime/2);
        SceneManager.LoadScene(lvlindex);
    }
    IEnumerator ReadyUp() { yield return new WaitForSeconds(0.5f);ready = true; }
    IEnumerator Exittrans()
    {
        LevelTransAnim.SetTrigger("ChangeScene");
        yield return new WaitForSeconds(transtime/2);
        GameObject.Find("MultiSceneSoundManager").GetComponent<AudioSource>().PlayOneShot(Swipesound, 0.2f);
        yield return new WaitForSeconds(transtime/2);
        Application.Quit();
    }
}
