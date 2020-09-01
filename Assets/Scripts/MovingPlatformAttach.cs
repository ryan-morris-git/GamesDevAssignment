using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformAttach : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;

    void OnTriggerEnter() {
        player.transform.parent.parent = platform.transform;
    }
    void OnTriggerExit() {
        player.transform.parent.parent = null;
    }
}
