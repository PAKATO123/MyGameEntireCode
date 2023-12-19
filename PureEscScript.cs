using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PureEscScript : MonoBehaviour
{
    public float transtime = 1;
    public Animator LevelTransAnim;
    public AudioSource MultiSceneSound;
    public AudioClip EscSwipe;
    private void Start()
    {
        MultiSceneSound = GameObject.Find("MultiSceneSoundManager").GetComponent<AudioSource>();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { LevelChange(); }
    }
    public void LevelChange() { StartCoroutine(leveltrans(0)); }
    IEnumerator leveltrans(int lvlindex)
    {
        LevelTransAnim.SetTrigger("ChangeScene");
        yield return new WaitForSecondsRealtime(transtime / 2);
        if (X()) { MultiSceneSound.PlayOneShot(EscSwipe, 0.2f); }
        yield return new WaitForSecondsRealtime(transtime / 2);
        Time.timeScale = 1;
        SceneManager.LoadScene(lvlindex);
    }
    bool X() {return GameObject.Find("MultiSceneSoundManager").GetComponent<AudioSource>(); }
    
  
}
