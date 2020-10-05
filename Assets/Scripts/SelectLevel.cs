using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
	public GameObject interactText;
	public GameObject missionSelectUI;


	public static bool isInSelection = false;
	private static bool nearMissionSelect = false;


	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (interactText)
			{
				interactText.SetActive(true);
			}
			nearMissionSelect = true;

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

		nearMissionSelect = false;

	}

	// Update is called once per frame
	void Update()
	{

		if (nearMissionSelect && Input.GetKeyDown(KeyCode.E))
		{
			Cursor.visible = true;
			Screen.lockCursor = false;
			EnterSelect();
		}
		if (isInSelection)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Cursor.visible = false;
				Screen.lockCursor = true;
				ExitSelect();
			}

		}

	}


	void EnterSelect()
	{
		

		interactText.SetActive(false);
		missionSelectUI.SetActive(true);
		Time.timeScale = 0f;
		Shooting.allowFire = false;
		isInSelection = true;
	}

	void ExitSelect()
	{
		

		interactText.SetActive(true);
		missionSelectUI.SetActive(false);
		Time.timeScale = 1f;
		Shooting.allowFire = true;
		isInSelection = false;
	}
}
