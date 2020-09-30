using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float rotSpeed = 8;
    public Transform target;
    public float distFromTarget = 2;
    public Vector2 yminmax = new Vector2 (-6, 85);
    public float rotationSmooth = .12f;
    Vector3 rotationSmoothVel;
    Vector3 currentRot;
    float mousex;
    float mousey;


    void Start() {
        Cursor.visible = false;
    }
    void LateUpdate() {
        mousex += Input.GetAxis("Mouse X");
        mousey -= Input.GetAxis("Mouse Y");
        mousey = Mathf.Clamp(mousey, yminmax.x, yminmax.y);

        currentRot = Vector3.SmoothDamp(currentRot, new Vector3 (mousey, mousex), ref rotationSmoothVel, rotationSmooth);

        Vector3 targetRotation = new Vector3 (mousey, mousex);
        transform.eulerAngles = targetRotation;
        transform.position = target.position - transform.forward * distFromTarget;
        
        if (CharacterControl.aiming == true) {
            distFromTarget = 2.5f;
            transform.position = new Vector3 (transform.position.x + 1f, transform.position.y + 1f, transform.position.z);
        } else {
            distFromTarget = 4;
            transform.position = target.position - transform.forward * distFromTarget;
        }
    } 
}
