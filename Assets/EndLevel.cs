using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndLevel : MonoBehaviour
{
    public GameObject EndLevelUI;


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Cursor.visible = true;
            Screen.lockCursor = false;
            Time.timeScale = 0f;
            EndLevelUI.SetActive(true);
        }
    }


    public void PlayerChoice(string levelName)
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
        SceneManager.LoadScene(levelName);
    }
}
