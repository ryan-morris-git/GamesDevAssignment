using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public Text Ammo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Shooting.selectedWeapon == 0) {
            Ammo.text = "Ammo: " + Shooting.bulletAmmo.ToString();
        }
        else if (Shooting.selectedWeapon == 1) {
            Ammo.text = "Ammo: " + Shooting.grenadeAmmo.ToString();
        }
    }
}
