using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{

    public GameObject interactText;
	public GameObject shopMenuUI;

	public int maxBulletAmmo = 20;
	public int maxGrenadeAmmo = 6;

	public string scrollDown;

	public static bool isInShop = false;
	private static bool nearShop = false;


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
		}

		nearShop = false;
		
	}

	// Update is called once per frame
	void Update()
    {
		
		if (nearShop && Input.GetKeyDown(KeyCode.E))
            {
				EnterShop();
            }
        if(isInShop)
        {
            if (Input.GetKeyDown(KeyCode.Escape)){
				ExitShop();
            }

        }

		navigateMenu();

    }


	void EnterShop()
    {
		interactText.SetActive(false);
		shopMenuUI.SetActive(true);
		Time.timeScale = 0f;
		Shooting.allowFire = false;
		isInShop = true;
    }

	void ExitShop()
    {
		interactText.SetActive(true);
		shopMenuUI.SetActive(false);
		Time.timeScale = 1f;
		Shooting.allowFire = true;
		isInShop = false;
	}


	public void buyBlasterAmmo()
    {
		if(Shooting.bulletAmmo != maxBulletAmmo)
        {
			Shooting.bulletAmmo = maxBulletAmmo;
        }
		else
        {

        }
		
    }

	public void buyBombAmmo()
    {
		if(Shooting.grenadeAmmo != maxGrenadeAmmo)
        {
			Shooting.grenadeAmmo = maxGrenadeAmmo;
        }
        else
        {

        }
		
    }


	void navigateMenu()
    {
		
    }

}
