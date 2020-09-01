using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float rotspeed = 2.0f;
    public float speed = 5.0f;
    public float jumpForce = 8.0f;
    private Rigidbody rb;
    private bool isGrounded;

    private int jumpCount;
    Transform camerat;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camerat = Camera.main.transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Call the player movement every update
        PlayerMove(); 
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            anim.SetBool("Running", true);
        }  else {
            anim.SetBool("Running", false);
        }
        // Checking if the player is grounded, if they are then it allows them to double jump
        if (isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space) & (jumpCount <= 1) ) {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCount += 1;
            }
        }
        if (Input.anyKey == false) {
            anim.Play("Normal Status", 0, 1);
        }
        float targetRotation = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg + camerat.eulerAngles.y;

    }

    void PlayerMove() {
        // defines horizontal and vertical controls
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        // defines the direction that the camera is facing
        Vector3 forward = camerat.forward;
        Vector3 right = camerat.right;
        // projects forward and right directions on y plane
        forward.y = 0;
        right.y = 0;
        // sets direction for player to move
        Vector3 dir = right * hor + forward * ver;
        // moves player
        transform.position += dir * speed * Time.deltaTime;
        // sets rotation to direction of camera
        if (dir != Vector3.zero)  {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(right * hor + forward * ver), 0.4f);
        }


    }
    // Detect collision with ground
     void OnCollisionEnter(Collision collision) {
         if ((collision.gameObject.tag == "Ground") || (collision.gameObject.tag == "Platform")) {
             isGrounded = true;
             jumpCount = 0;
         }
     }
    // Detect collision exit with ground
     void OnCollisionExit(Collision collision) {
         if (jumpCount > 1) {
             isGrounded = false;
         }
     }
}
