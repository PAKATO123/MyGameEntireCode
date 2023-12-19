using UnityEngine;
using static UnityEngine.ParticleSystem;


public class ParticleAdjuster : MonoBehaviour
{
    public GameObject ParentAlpha;
    public ParticleSystem partsys;
    public ParticleSystem.EmissionModule emission;
    public ParticleSystem.ShapeModule PSshape;
    public float EmitterSize, AlphaSize,baseRate;
    void Start()
    {
        if (ParentAlpha.transform.localScale.x < 0) { gameObject.transform.localScale = new Vector3(-1, 1, 1); }
        partsys = gameObject.GetComponent<ParticleSystem>();
        PSshape = partsys.shape;
        emission = partsys.emission;
        EmitterSize = gameObject.transform.localScale.x * gameObject.transform.localScale.y;
        AlphaSize = ParentAlpha.transform.localScale.x * ParentAlpha.transform.localScale.y;
        SetValue();
    }
    void SetValue()
    {
        emission.rateOverTime = baseRate * (AlphaSize / EmitterSize);
        PSshape.scale = new Vector3(ParentAlpha.transform.localScale.x - 0.2f, ParentAlpha.transform.localScale.y - 0.2f, 1);
    }

}
