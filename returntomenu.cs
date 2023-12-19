using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returntomenu : MonoBehaviour
{
    public float transtime = 1, PrePauseTime;
    public Animator LevelTransAnim;
    public AudioSource MultiSceneSound;
    public AudioClip EscSwipe;
    public bool IsPaused = false, TimeControllerIsPaused;
    public GameObject TimePauser;
    private void Start()
    {
        if (ISthereMulti() == true) { MultiSceneSound = GameObject.Find("MultiSceneSoundManager").GetComponent<AudioSource>(); }
        if (ISthereTimeController()) { TimePauser = GameObject.Find("TimeController"); }
        TimeControllerIsPaused = TimePauser.GetComponent<TimeController>().IsPaused;
    }
    bool ISthereMulti() { return GameObject.Find("MultiSceneSoundManager").GetComponent<AudioSource>(); }
    bool ISthereTimeController() { return GameObject.Find("TimeController").GetComponent<TimeController>(); }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&GameObject.FindGameObjectWithTag("Player").GetComponent<Input_recive>().Alive) { PauseGame(); }
        if ((Input.GetKeyDown(KeyCode.Keypad1)|| Input.GetKeyDown(KeyCode.Alpha1)) && IsPaused) { LevelChange(); }
    }
    public void LevelChange() { StartCoroutine(leveltrans(0)); }
    IEnumerator leveltrans(int lvlindex)
    {
        LevelTransAnim.SetTrigger("ChangeScene");
        yield return new WaitForSecondsRealtime(transtime / 2);
        if (ISthereMulti() == true) { MultiSceneSound.PlayOneShot(EscSwipe, 0.2f); }
        yield return new WaitForSecondsRealtime(transtime / 2);
        Time.timeScale = 1;
        SceneManager.LoadScene(lvlindex);
    }
    void PauseGame()
    {
        
        if (!IsPaused)
        {
            TimeControllerIsPaused = true;
            PrePauseTime = Time.timeScale;
            Time.timeScale = 0;
            IsPaused = !IsPaused;
            GameObject.Find("PauseScreen").GetComponent<Animator>().SetTrigger("Pause");
        }
        else {
            TimeControllerIsPaused = false;
            Time.timeScale = PrePauseTime;
            IsPaused = !IsPaused;
            GameObject.Find("PauseScreen").GetComponent<Animator>().SetTrigger("Resume");
        }
    }
}
