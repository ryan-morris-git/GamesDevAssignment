using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject grenade;
    public static int selectedWeapon = 0;
    public static int bulletAmmo = 20;
    public static int grenadeAmmo = 6;

    public static bool allowFire = true;

    public float power = 1500f;
    public float moveSpeed = 12f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(allowFire)
        {
            SelectWeapon();
            if (Input.GetButtonUp("Fire1") && (selectedWeapon == 0) && (bulletAmmo > 0)) {
                GameObject instance = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
                Vector3 fwd = transform.TransformDirection(Vector3.forward);
                instance.GetComponent<Rigidbody>().AddForce(fwd * power);
                bulletAmmo -= 1;
            }
            else if (Input.GetButtonUp("Fire1") && (selectedWeapon == 1) && (grenadeAmmo > 0)) {
                GameObject instance = Instantiate(grenade, transform.position, transform.rotation) as GameObject;
                Vector3 fwd = transform.TransformDirection(Vector3.forward);
                instance.GetComponent<Rigidbody>().AddForce(fwd * power);
                grenadeAmmo -= 1;
            }
        }

        if (bulletAmmo > 20) {
            bulletAmmo = 20;
        }
        if (grenadeAmmo > 6) {
            grenadeAmmo = 6;
        }
        
    }
    void SelectWeapon() {
        if ((Input.GetAxis("Mouse ScrollWheel") != 0.0f) && (selectedWeapon == 0)) {
            selectedWeapon = 1;
        }
        else if ((Input.GetAxis("Mouse ScrollWheel") != 0.0f) && (selectedWeapon == 1)) {
            selectedWeapon = 0;
        }
    }
}
