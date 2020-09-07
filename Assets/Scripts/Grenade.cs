using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explodeTime = 3.0f;
    public float elapsedTime;
    public float radius = 5.0f;
    public float explosionForce = 10.0f;
    bool hasExploded = false;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if ((elapsedTime >= explodeTime) && !hasExploded) {
            Explode();
            hasExploded = true;
        }
    }

    void Explode() {
        // Creates instance of the explosion
        Instantiate(explosion, transform.position, transform.rotation);

        // Finds all objects that the explosion is touching and adds them to an array, then adds explosion force to each item in the array
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders) {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }

        Destroy(this.gameObject);
    }
}
