using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpawn : MonoBehaviour
{
   void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<CharacterControl>().RespawnLocation = gameObject;
        }

    }
}
