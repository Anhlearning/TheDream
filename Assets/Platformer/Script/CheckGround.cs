using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
   Rigidbody rb;
    public Vector3 boxSize;
    public float maxDistance;   
    public LayerMask layerMask;
    public bool grounded;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(IsGrounded())
        {
            Debug.Log("Grounded");
            rb.velocity = Vector3.zero;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
    bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, boxSize, Vector3.down, Quaternion.identity, maxDistance, layerMask);
    }
}
