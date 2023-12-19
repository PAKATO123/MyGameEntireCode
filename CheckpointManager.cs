using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform SP;
    public GameObject Spawnpoint;
    public void Start()
    {
        
        SP = Spawnpoint.transform;
    }
}
