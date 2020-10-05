using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public float rotationSpeed = 1.5f;
    public AudioClip collectSound;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime * 100, 0);
        ;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            Destroy(this.gameObject);
        }
        
    }
}
