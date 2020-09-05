using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPhysics : MonoBehaviour
{
    public float fallSpeed = 1.5f;

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
            rb.AddForce(Vector3.down * fallSpeed, ForceMode.VelocityChange);
            //rb.velocity += Vector3.up * Physics.gravity.y * (fallSpeed - 1) * Time.deltaTime;
        }
    }
}