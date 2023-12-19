using UnityEngine;

public class PlatformStandCheck : MonoBehaviour
{ public Collider2D platcollider;
    public float DisUp;
    [SerializeField] public LayerMask PlayerMask;
    private void Start()
    {
        platcollider = gameObject.GetComponent<Collider2D>();
    }
    public bool IsPlayerAbove() { return Physics2D.BoxCast
            (platcollider.bounds.center + new Vector3(0, platcollider.bounds.extents.y, 0),
            new Vector3(platcollider.bounds.size.x, DisUp, platcollider.bounds.size.z), 0f,
            Vector2.up,
            DisUp / 2 + platcollider.bounds.extents.y,
            PlayerMask);}
}
