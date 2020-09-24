using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    public Image AimReticle;
    // Start is called before the first frame update
    void Start()
    {
        AimReticle = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterControl.aiming == true) {
            AimReticle.color = new Color(AimReticle.color.r, AimReticle.color.g, AimReticle.color.b, 1.0f);
        } else {
            AimReticle.color = new Color(AimReticle.color.r, AimReticle.color.g, AimReticle.color.b, 0.0f);
        }
    }
}
