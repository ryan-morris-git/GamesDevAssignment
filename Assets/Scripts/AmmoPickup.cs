using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,2,0);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            if ((Shooting.bulletAmmo < 20) || (Shooting.grenadeAmmo < 6)) {
                Shooting.bulletAmmo += 5;
                Shooting.grenadeAmmo += 1;
                Destroy(this.gameObject);
            }
        }
    }
}
