using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = .05f;

    CapsuleCollider2D touchingCol;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];    

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance);
    }
}
