using UnityEngine;
using System.Collections;
using System.Linq;

public class SimpleFSM : FSM
{
	public enum FSMState
	{
		None,
		Patrol,
		Attack,
		Chase,
		Dead,
	}

	//track the current state
	public FSMState curState;

	//enemy speed
	public float speed = 0.0f;

	//enemy speed of rotation
	public float rotSpeed = 4.0f;

	//bullet
	public GameObject bullet;
	public Transform bulletSpawnPoint;
	public float shootRate;
	public float elapsedTime;

	//Tracking the life of the enemy
	private bool isDead = false;
	public int health = 30;

	public float playerDistance;
	//private GameObject player;


	//Initialize the object
	protected override void Initialize ()
	{
		shootRate = bullet.GetComponent<Bullet>().shootRate;

		//set the points for patrol
		//waypoints = GameObject.FindGameObjectsWithTag ("waypoint");
		Transform parentWaypoint = transform.parent.FindChild ("Waypoints");
		waypoints = parentWaypoint.gameObject.GetComponentsInChildren<Transform> ();

		//set the first point
		//FindNextPoint ();

		//find the player
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;

		//set the weapon
		//weapons = gameObject.transform.FindChild("Weapon").transform;
		bulletSpawnPoint = transform.GetComponentsInChildren<Transform> ().FirstOrDefault (t => t.name == "BulletSpawnPoint");
		//bulletSpawnPoint = transform.GetChild (0).GetChild (0);
		destPos = playerTransform.position;
	}

	//updates per frame
	protected override void FSMUpdate ()
	{
		switch (curState) 
		{
		case FSMState.Attack: 
			AttackUpdate(); 
			break;
		case FSMState.Chase:
			ChaseUpdate();
			break;
		case FSMState.Patrol:
			PatrolUpdate();
			break;
		case FSMState.Dead: 
			DeadUpdate();
			break;
		case FSMState.None:
			NoStateUpdate();
			break;
		}
		playerDistance = Vector3.Distance (transform.position, playerTransform.position);
		//update the time
		elapsedTime += Time.deltaTime;

		//check if we have died
		if(health <= 0)
		{
			curState = FSMState.Dead;
		}
	}

	protected void NoStateUpdate()
	{
		//used for testing states
		if(playerDistance < 5.0f)
		{
			curState = FSMState.Attack;
		}
		else if(playerDistance <= 20)
		{
			//curState = FSMState.Chase;
		}
	}

	//update the attack state
	protected void AttackUpdate()
	{
		//destination is the player
		destPos = playerTransform.position;

		//check the distance of the player to us
		float distance = Vector3.Distance (transform.position, destPos);

		//rotate towards the player
		Quaternion enemyRotation = Quaternion.LookRotation(transform.position - playerTransform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, enemyRotation, Time.deltaTime * rotSpeed);
		//weapon isn't separate so I am rotating the spawn point
		Quaternion weaponRotation = Quaternion.LookRotation (playerTransform.position - bulletSpawnPoint.position);
		bulletSpawnPoint.rotation = Quaternion.Slerp (bulletSpawnPoint.rotation, weaponRotation, Time.deltaTime * rotSpeed);

		if (distance > 5.0f || distance <= 20.0f) 
		{
			//move forward
			//transform.Translate(Vector3.forward * Time.deltaTime * speed);

			//keep us in the attack mode
			//Debug.Log("Chase");
			//curState = FSMState.Chase;
		}
		//player is out of range
		if (distance > 20.0f) 
		{
			//switch to patrol
			//curState = FSMState.Patrol;
			//Debug.Log("Patrol");
			curState = FSMState.None;
		}

		//rotate weapon if desired

		//shoot bullet
		ShootBullet ();
	}

	//update the patrol state
	protected void PatrolUpdate()
	{
		//move to next patrol point of position is reached
		if (Vector3.Distance (transform.position, destPos) <= 5.0f) 
		{
			FindNextPoint ();
		}
		//see if we are near the player
		else if (Vector3.Distance (transform.position, playerTransform.position) <= 20.0f) 
		{
			//switch to chase state
			curState = FSMState.Chase;
		}

		//rotate towards the destination
		Quaternion targetRotation = Quaternion.LookRotation (transform.position - destPos);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotSpeed);

		//move forward
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
    }

	protected void ChaseUpdate()
	{
		//set the destination to the player
		destPos = playerTransform.position;

		//check to see how close we are to the player
		float distance = Vector3.Distance (transform.position, destPos);
		if (distance <= 5.0f) 
		{
			//switch to attack mode
			curState = FSMState.Attack;
		}
		else if(distance > 20.0f)
		{
			//switch to patrol
			//curState = FSMState.Patrol;
			curState = FSMState.None;
		}

		//rotate toward target
		Quaternion targetRotation = Quaternion.LookRotation (transform.position - destPos);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
		
		//move forward
		transform.Translate (Vector3.back * Time.deltaTime * speed);
	}

	protected void DeadUpdate()
	{
		//do what we need with the dead enemy
		Destroy (gameObject); 
	}

	protected void ShootBullet()
	{
		//check to see if we can fire a bullet
		if(elapsedTime >= shootRate)
		{
			Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
			elapsedTime = 0.0f;
		}
	}

	protected void OnCollisionEnter(Collision collision)
	{
		//reduce health
		if(collision.gameObject.tag == "Bullet")
		{
			health -= collision.gameObject.GetComponent<Bullet>().damage;
		}
	}

	protected void FindNextPoint()
	{
		int rndIndex = Random.Range(0, waypoints.Length);
		float rndRadius = 10.0f;
		
		Vector3 rndPosition = Vector3.zero;
		destPos = waypoints[rndIndex].transform.position + rndPosition;
		
		//Check Range
		//Prevent to decide the random point as the same as before
		if (true)//IsInCurrentRange(destPos))
		{
			rndPosition = new Vector3(Random.Range(-rndRadius, rndRadius), 0.0f, Random.Range(-rndRadius, rndRadius));
			destPos = waypoints[rndIndex].transform.position + rndPosition;
		}
	}

	//protected bool IsInCurrentRange(Vector3 pos)
	//{
		//float xPos = Mathf.Abs(pos.x - transform.position.x);
		//float zPos = Mathf.Abs(pos.z - transform.position.z);
		
		//if (xPos <= 50 && zPos <= 50)
			//return true;
		
		//return false;
	//}
}
