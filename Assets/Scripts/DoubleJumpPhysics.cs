using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpPhysics : MonoBehaviour
{
    public float fallSpeed = 3.0f;
    public float shortHop = 2.5f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallSpeed - 1) * Time.deltaTime;
        }
    }
}
