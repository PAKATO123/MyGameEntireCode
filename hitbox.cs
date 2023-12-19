using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    public BoxCollider2D InvisCollider;
    public bool Active;

    // Update is called once per frame
    void Update()
    { if (Active){ 
        Debug.DrawRay(InvisCollider.bounds.center + new Vector3(InvisCollider.bounds.extents.x, 0, 0), Vector2.down * InvisCollider.bounds.extents.y);
        Debug.DrawRay(InvisCollider.bounds.center - new Vector3(InvisCollider.bounds.extents.x, 0, 0), Vector2.down * InvisCollider.bounds.extents.y);
        Debug.DrawRay(InvisCollider.bounds.center + new Vector3(InvisCollider.bounds.extents.x, 0, 0), Vector2.up * InvisCollider.bounds.extents.y);
        Debug.DrawRay(InvisCollider.bounds.center - new Vector3(InvisCollider.bounds.extents.x, 0, 0), Vector2.up * InvisCollider.bounds.extents.y);
        Debug.DrawRay(InvisCollider.bounds.center + new Vector3(0, InvisCollider.bounds.extents.y, 0), Vector2.left * InvisCollider.bounds.extents.x);
        Debug.DrawRay(InvisCollider.bounds.center - new Vector3(0, InvisCollider.bounds.extents.y, 0), Vector2.left * InvisCollider.bounds.extents.x);
        Debug.DrawRay(InvisCollider.bounds.center + new Vector3(0, InvisCollider.bounds.extents.y, 0), Vector2.right * InvisCollider.bounds.extents.x);
        Debug.DrawRay(InvisCollider.bounds.center - new Vector3(0, InvisCollider.bounds.extents.y, 0), Vector2.right * InvisCollider.bounds.extents.x);
    }
    }
}
