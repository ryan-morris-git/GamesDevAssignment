﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3.0f;
    public float elapsedTime;
    public int damage = 20;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;
        source = GetComponent<AudioSource>();
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }   
}
