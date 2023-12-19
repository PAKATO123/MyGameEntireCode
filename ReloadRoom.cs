using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ReloadRoom : MonoBehaviour
{
    [SerializeField]
    public GameObject Player,GOanimation;
    private Vector3 trans;
    private bool Activating;
    public int Time;
    // Start is called before the first frame update
    void Start()
    {  
        trans = gameObject.transform.position;
        Activating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<CheckpointSystem>().Reloadroom == true&&Activating == false)
        {
            ReloadAct();
        }
    }
    async void ReloadAct() {
        Activating = true; gameObject.transform.position = trans;
        GOanimation.GetComponent<Animator>().SetTrigger("RestState");
        await Task.Delay(Time);
        GOanimation.GetComponent<Animator>().ResetTrigger("RestState");
        Activating = false;
    }
}
