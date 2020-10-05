using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,2);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            if (CharacterControl.playerHealth < 100) {
                CharacterControl.playerHealth += 20;
                Destroy(this.gameObject);
            }
        }
    }
}
