using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFSM : MonoBehaviour
{
    // Defining the FSM
     public enum FSMState
    {
        Walk,
        Flee,
    }

    // Default state is just walking
    public FSMState curState;

    // Movement Speed
    public float moveSpeed = 3.0f;
    // Rotation speed
    public float rotSpeed = 6.0f;

    // Destination position
    protected Vector3 destPos;
    // List of destination points
    protected GameObject[] pointList;
    private GameObject Guard;
    private GameObject player;
    public static bool guardNotified;

    
    
    // Start is called before the first frame update
    void Start()
    {
        guardNotified = false;

        curState = FSMState.Walk;

        pointList = GameObject.FindGameObjectsWithTag("NPCWalkPoint");
        FindNextPoint(); 

        Guard = GameObject.FindGameObjectWithTag("Guard");

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (curState)
        {
            case FSMState.Walk: UpdateWalkState(); break;
            case FSMState.Flee: UpdateFleeState(); break;
        }
        
        if (Vector3.Distance(transform.position, destPos) <= 1.0f) {
            FindNextPoint();
        }

        if (Vector3.Distance(transform.position, Guard.transform.position) <= 2.0f) {
            guardNotified = true;
            moveSpeed = 5.0f;
            RunAway();
        }

        if (guardNotified == true) {
            RunAway();
        }
    }

    void Move() {
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * Time.deltaTime * moveSpeed);
    }

    void RunAway() {
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - GameObject.FindGameObjectWithTag("Player").transform.position);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * Time.deltaTime * moveSpeed);
    }

    void FindNextPoint() {
        int rndIndex = Random.Range(0, pointList.Length);
        destPos = pointList[rndIndex].transform.position;
    }

    void UpdateWalkState() {
        Move();
    }

    void UpdateFleeState() {
        destPos = new Vector3(Guard.transform.position.x, 0.0f, Guard.transform.position.z);
        Move();
    }

    protected void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bullet") {
            Destroy(other);
            curState = FSMState.Flee;
            moveSpeed = 5.0f;
        }
    }
}
