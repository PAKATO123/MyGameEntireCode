
using UnityEngine;
public class CamReloadRest : MonoBehaviour
{
    public Animator X;
    // Start is called before the first frame update
    void CamReloadRestFunc() { X.SetTrigger("ReloadReset"); }
}
