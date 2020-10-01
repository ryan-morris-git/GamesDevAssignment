using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformAttach : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;

    void Start() {
        
    }

    // NPCs seem to trigger it as well??
    void OnTriggerEnter() {
        player = GameObject.FindWithTag("Player");
        player.transform.parent.parent = platform.transform;
    }
    void OnTriggerExit() {
        player = GameObject.FindWithTag("Player");
        player.transform.parent.parent = null;
    }
}
