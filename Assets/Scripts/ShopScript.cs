﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{

    public GameObject interactText;
	public GameObject thePlayer;
	public GameObject shopMenuUI;

	public static bool isInShop = false;
	public static bool nearShop = false;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (interactText)
			{
				interactText.SetActive(true);
			}
			nearShop = true;
		}
	}


	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (interactText)
			{
				interactText.SetActive(false);
			}
			nearShop = true;
		}

		
	}

	// Update is called once per frame
	void Update()
    {
		if(nearShop && Input.GetKeyDown(KeyCode.E))
        {
			EnterShop();
        }

        if(isInShop)
        {
            if (Input.GetKeyDown(KeyCode.Escape)){
				ExitShop();
            }

        }

    }


	void EnterShop()
    {
		shopMenuUI.SetActive(true);
		Time.timeScale = 0f;
		isInShop = true;
    }

	void ExitShop()
    {
		shopMenuUI.SetActive(false);
		Time.timeScale = 1f;
		isInShop = false;
	}

}