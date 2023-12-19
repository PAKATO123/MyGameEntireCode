using UnityEngine;

public class RandomAnimationStart : MonoBehaviour
{
    private Animator X;
    // Start is called before the first frame update
    void Start()
    {
        X = GetComponent<Animator>();
        var state = X.GetCurrentAnimatorStateInfo(0);
        X.Play(state.fullPathHash,0,Random.Range(0f,1f));
       
    }

}
