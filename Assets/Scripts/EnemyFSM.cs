using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum FSMState
    {
        None,
        Patrol,
        Dead,
        Chase,
        Attack,
    }

    public FSMState curState;

    // Movement Speed
    public float moveSpeed = 5.0f;
    // Rotation speed
    public float rotSpeed = 6.0f;
    // Chase range
    public float chaseRange = 35.0f;
    // Stop range
    public float stopRange = 20.0f;

    // Player transform
    protected Transform playerTransform;
    // Destination position
    protected Vector3 destPos;
    // List of destination points
    protected GameObject[] pointList;

    Animator anim;

    // Boolean checking whether or not enemy is dead
    protected bool bDead;
    protected bool moving = false;
    public int health = 20;
    private Rigidbody rb;
    public float shootSpeed = 2.0f;
    private float elapsedTime;
    private GameObject Player;
    
    public GameObject bulletSpawnPoint;
    public GameObject bullet;
    public float power = 1500.0f;

    // Start is called before the first frame update
    void Start()
    {
        curState = FSMState.Patrol;

        bDead = false;

        pointList = GameObject.FindGameObjectsWithTag("PatrolPoint");
        FindNextPoint(); 

        Player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = Player.transform;
        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        elapsedTime = shootSpeed;

        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        switch (curState)
        {
            case FSMState.Patrol: UpdatePatrolState(); break;
            case FSMState.Dead: UpdateDeadState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
            case FSMState.Attack: UpdateAttackState(); break;
        }

        // Go to dead state if no health left
        if (health <= 0) {
            curState = FSMState.Dead;
        }
        // Go back to patrolling if out of range
        if ((distance > chaseRange) & (health > 0)) {
            curState = FSMState.Patrol;
        }
        // Go to chase state if within range
        if ((distance <= chaseRange) & (health > 0)) {
            curState = FSMState.Chase;
        }
        // Go to attack state if within range
        if ((distance <= stopRange) & (health > 0)) {
            curState = FSMState.Attack;
        }
    }

    protected void Move() {
        if (moving) {
            Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
            GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * Time.deltaTime * moveSpeed);
            anim.SetBool("Running", true);
        }
     }
    void UpdatePatrolState() {
        if (Vector3.Distance(transform.position, destPos) <= 1.0f) {
            FindNextPoint();
        }
        moving = true;
        Move();
    }

    void UpdateAttackState() {
        if (Vector3.Distance(transform.position, playerTransform.position) <= stopRange) {
            destPos = new Vector3(Player.transform.position.x, 0.0f, Player.transform.position.z);
            Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
            Move();
            moving = false;
        }
        //Attack the player at a set interval
        if (elapsedTime >= shootSpeed) {
				//Reset the time
				elapsedTime = 0.0f;

				if ((bulletSpawnPoint) & (bullet)) {
					GameObject instance = Instantiate(bullet, bulletSpawnPoint.transform.position, transform.rotation);
                    Vector3 fwd = transform.TransformDirection(Vector3.forward);
                    instance.GetComponent<Rigidbody>().AddForce(fwd * power);
                }
            }

		// Update the time
		elapsedTime += Time.deltaTime;
    }

    void UpdateChaseState() {
        if (Vector3.Distance(transform.position, playerTransform.position) <= chaseRange) {    
            destPos = new Vector3(Player.transform.position.x, 0.0f, Player.transform.position.z);
            Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
            Move(); 
        }
    }

    void UpdateDeadState() {
        bDead = true;
    }
    protected void FindNextPoint()
    {
        int rndIndex = Random.Range(0, pointList.Length);
        destPos = pointList[rndIndex].transform.position;
    }
}
