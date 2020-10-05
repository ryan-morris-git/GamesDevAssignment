using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum FSMState
    {
        None,
        Wait,
        Dead,
        Chase,
        Attack,
        Flee,
    }

    public FSMState curState;

    // Movement Speed
    public float moveSpeed;
    // Rotation speed
    public float rotSpeed = 16.0f;
    // Chase range
    public float chaseRange = 20.0f;
    // Stop range
    public float stopRange = 10.0f;

    // Player transform
    protected Transform playerTransform;
    // Destination position
    protected Vector3 destPos;
    // List of destination points
    protected GameObject[] pointListEnemy;

    Animator anim;

    // Boolean checking whether or not enemy is dead
    public bool bDead; //change to public for Spawning acceess
    protected bool moving = false;
    public int health;
    private Rigidbody rb;
    public float shootSpeed = 2.0f;
    private float elapsedTime;
    private GameObject Player;
    
    public GameObject bulletSpawnPoint;
    public GameObject bullet;
    public float power = 1000.0f;
    public GameObject enemyExplosion;

    // Start is called before the first frame update
    void Start()
    {
        curState = FSMState.Wait;

        bDead = false;

        moveSpeed = 8.0f;

        pointListEnemy = GameObject.FindGameObjectsWithTag("EnemyPatrolPoint");

        Player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = Player.transform;
        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        elapsedTime = shootSpeed;

        anim = GetComponent<Animator>();

        health = 20;
    }


    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        destPos = playerTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));
        

        switch (curState)
        {
            case FSMState.Wait: UpdateWaitState(); break;
            case FSMState.Dead: UpdateDeadState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
            case FSMState.Attack: UpdateAttackState(); break;
            case FSMState.Flee: UpdateFleeState(); break;
        }

        // Go to dead state if no health left
        if (health <= 0) {
            curState = FSMState.Dead;
        }
        // Go back to waiting if out of range
        if ((distance > chaseRange) & (health > 0)) {
            curState = FSMState.Wait;
        }
        // Go to chase state if within range
        if ((distance <= chaseRange) & (health > 0)) {
            curState = FSMState.Chase;
        }
        // Go to attack state if within range
        if ((distance <= stopRange) & (health > 0)) {
            curState = FSMState.Attack;
        }
        // Flee if boss is dead
        if ((EnemyBossFSM.bossDead == true) & (health > 0)) {
            curState = FSMState.Flee;
        }

        if (Vector3.Distance(transform.position, playerTransform.position) >= 100.0f) {
            this.gameObject.SetActive(false);
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
    void UpdateWaitState() {
        destPos = playerTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));
    }

    void UpdateAttackState() {
        if (Vector3.Distance(transform.position, playerTransform.position) <= stopRange) {
            destPos = playerTransform.position;
            Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
            moving = false;
        } else {
            moving = true;
        }
        //Attack the player at a set interval
        if (elapsedTime >= shootSpeed) {
				//Reset the time
				elapsedTime = 0.0f;

				if ((bulletSpawnPoint) & (bullet)) {
					GameObject instance = Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
                    Vector3 fwd = (playerTransform.position - bulletSpawnPoint.transform.position).normalized;
                    instance.GetComponent<Rigidbody>().AddForce(fwd * power);
                }
            }

		// Update the time
		elapsedTime += Time.deltaTime;
    }

    void UpdateChaseState() {
        if (Vector3.Distance(transform.position, playerTransform.position) <= chaseRange) {    
            moving = true;
            destPos = playerTransform.position;
            Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
            Move(); 
        }
        if (Vector3.Distance(transform.position, playerTransform.position) >= chaseRange) {  
            curState = FSMState.Wait;
        }
    }

    void UpdateDeadState() {
        bDead = true;
        Destroy(this.gameObject);
        Instantiate(enemyExplosion, transform.position, transform.rotation);
    }

    void UpdateFleeState() {
        moveSpeed = 14.0f;
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - playerTransform.position);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * Time.deltaTime * moveSpeed);
        anim.SetBool("Running", true);
    }
    protected void FindNextPoint()
    {
        if (pointListEnemy.Length > 0) {
            int rndIndex = Random.Range(0, pointListEnemy.Length);
            destPos = pointListEnemy[rndIndex].transform.position;
        } 
    }


    protected void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bullet") {
            Destroy(other);
            health -= 5;
        }
        if (other.gameObject.tag == "Grenade") {
            health -= 15;
        }
    }
}
