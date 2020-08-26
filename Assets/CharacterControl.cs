using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    public float speed = 5.0f;
    public float jumpForce = 8.0f;
    private Rigidbody rb;
    private bool isGrounded;

    private int jumpCount;
    Transform camerat;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camerat = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Call the player movement every update
        PlayerMove();

        // Checking if the player is grounded, if they are then it allows them to double jump
        if (isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space) & (jumpCount <= 1) ) {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCount += 1;
            }
        }
        float targetRotation = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg + camerat.eulerAngles.y;
    }

    void PlayerMove() {
        // Basic movement script
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMove = new Vector3(hor, 0, ver) * speed * Time.deltaTime;
        transform.Translate(playerMove, Space.Self);
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
