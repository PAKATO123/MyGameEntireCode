using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class ADsequence : MonoBehaviour
{
    Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    async void sequence()
    { await Task.Delay(299);
        anim.Stop();
    }
}
