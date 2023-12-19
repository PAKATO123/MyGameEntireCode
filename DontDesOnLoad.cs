using UnityEngine;

public class DontDesOnLoad : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(this);  
    }


}
