using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawing : MonoBehaviour
{
    public GameObject[] EnemySquad;

    private bool EngagePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EngagePlayer)
        {
            foreach(GameObject Enemy in EnemySquad)
            {
                if (Enemy) {
                    if(Enemy.GetComponent<EnemyFSM>().bDead == false)
                    {
                        Enemy.SetActive(true);
                    } else {
                        Enemy.SetActive(false);
                    }
                }
            }
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            EngagePlayer = true;
        }
    }

}
