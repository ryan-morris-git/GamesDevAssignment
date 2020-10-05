using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3.0f;
    public float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= lifeTime)
        {
            Destroy(this.gameObject);
        }

        //if (Physics.Raycast(transform.position, transform.forward, 1))
        //{
            //Destroy(this.gameObject);
        //}
    }
    void OnCollisionEnter (Collider other) {
        Destroy(this.gameObject);
    }
   
}
