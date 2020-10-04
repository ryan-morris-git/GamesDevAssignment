using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<CharacterControl>().RespawnPlayer();
        }
    }
}
