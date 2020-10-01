using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformAttach : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;

    void Start() {
        
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            player.transform.parent.parent = platform.transform;
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            player.transform.parent.parent = null;
        }
    }
}
