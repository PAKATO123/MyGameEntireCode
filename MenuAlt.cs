using UnityEngine;

public class MenuAlt : MonoBehaviour
{
    private void Update()
    {
        if (PlayerPrefs.GetInt("savedlevel") > 1)
        { gameObject.GetComponent<Animator>().SetTrigger("Alter"); }

    }

}
