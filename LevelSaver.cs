using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSaver : MonoBehaviour
{public int savedLevel;
    void Start()
    {
        PlayerPrefs.SetInt ( "savedlevel", SceneManager.GetActiveScene().buildIndex);
    }
}
