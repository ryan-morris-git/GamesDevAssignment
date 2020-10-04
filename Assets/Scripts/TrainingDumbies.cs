using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDumbies : MonoBehaviour
{

    private int healthpool;
    public int healthpoolSize = 100;
    public float respawnTimer = 60f;
    public GameObject dummy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        noHealth();

        //respawnObject();
    }

    

    void noHealth()
    {
        if(healthpool == 0)
        {
            dummy.SetActive(false);
        }
    }

    void respawnObject()
    {
        if (respawnTimer == 60)
        {
            healthpool = healthpoolSize;
            dummy.SetActive(true);
        }
    }

    public void takeDamage(int damage)
    {
        healthpool -= damage;
    }

}
