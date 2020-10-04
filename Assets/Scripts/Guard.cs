using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{

    // Defining the FSM
     public enum FSMState
    {
        Watch,
        Attack,
    }

    // Default state is just walking
    public FSMState curState;

    // Movement Speed
    public float moveSpeed = 8.0f;
    // Rotation speed
    public float rotSpeed = 10.0f;

    public float stopDist = 0.0f;

    // Destination position
    protected Vector3 destPos;
    // List of destination points
    private GameObject player;

    private Rigidbody rigbod;


    // Start is called before the first frame update
    void Start()
    {
        curState = FSMState.Watch;

        player = GameObject.FindGameObjectWithTag("Player");

        destPos = player.transform.position;

        rigbod = GetComponent<Rigidbody>();

        rigbod.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        destPos = player.transform.position;
        
        switch (curState)
        {
            case FSMState.Watch: UpdateWatchState(); break;
            case FSMState.Attack: UpdateAttackState(); break;
        }

        if (NPCFSM.guardNotified == true) {
            curState = FSMState.Attack;
        }
        
    }

    void Move() {
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void UpdateWatchState() {
        destPos = player.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));
    }
    
    void UpdateAttackState() {
        if (Vector3.Distance(transform.position, player.transform.position) >= stopDist) {
            Move();
            destPos = player.transform.position;
            rigbod.constraints = ~RigidbodyConstraints.FreezePosition;
        }
    }
}
