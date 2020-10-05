using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{

    public GameObject interactText;
	public GameObject ShopKeeperText;
    public GameObject shopMenuUI;

	public GameObject shopkeeper;

	public int maxBulletAmmo = 20;
	public int maxGrenadeAmmo = 6;

	public string scrollDown;

	public static bool isInShop = false;
	private static bool nearShop = false;


	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			if ((Shooting.bulletAmmo < maxBulletAmmo || Shooting.grenadeAmmo < maxGrenadeAmmo))
			{
				nearShop = true;
				interactText.SetActive(true);
			}
			else
            {
				ShopKeeperText.SetActive(true);
            }
			

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
			ShopKeeperText.SetActive(false);
		}

		nearShop = false;
		
	}

	// Update is called once per frame
	void Update()
    {
		//checks to see if the user can indeed buy ammo (Shop keeper NPC)
		if (Shooting.bulletAmmo >= maxBulletAmmo && Shooting.grenadeAmmo >= maxGrenadeAmmo)
        {
			shopkeeper.SetActive(true);
        }
        else
        {
			shopkeeper.SetActive(false);
        }
			//checks if the user is near the shop to allow for interact E to open shop
			if (nearShop && Input.GetKeyDown(KeyCode.E))
            {
				Cursor.visible = true;
				Screen.lockCursor = false;
				EnterShop();
            }
        if(isInShop)
        {
            if (Input.GetKeyDown(KeyCode.Escape)){
				Cursor.visible = false;
				Screen.lockCursor = true;
				ExitShop();
            }

        }

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
		
    }

	public void buyBombAmmo()
    {
		if(Shooting.grenadeAmmo != maxGrenadeAmmo)
        {
			Shooting.grenadeAmmo = maxGrenadeAmmo;
        }
       
		
    }



}
