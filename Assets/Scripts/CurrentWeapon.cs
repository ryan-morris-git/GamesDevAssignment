using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeapon : MonoBehaviour
{
    public Text curWeapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Shooting.selectedWeapon == 0) {
            curWeapon.text = "Weapon: Blaster";
        }
        else if (Shooting.selectedWeapon == 1) {
            curWeapon.text = "Weapon: Grenade";
        }
    }
}