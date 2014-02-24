using UnityEngine;
using System.Collections;
using System.Linq;

public class HitState : FSMState
{
	protected float elapsedTime = 0.0f;
	public float duration = 10.0f;

	public override void Construct ()
	{
		this.stateId = FSMStateId.Hit;
		if(this.transform.GetComponent<EnemyInfo>().platform != null)
		{
			this.DetermineBoundries ();
		}
	}	

	void Update()
	{
		if(this.transform.GetComponent<EnemyInfo>().platform != null)
		{
			this.DetermineBoundries ();
		}
	}

	public override void Act (Transform player)
	{
		//rotate the enemy towards they player
		this.transform.RotateYToward (player, this.GetComponent<EnemyInfo>().rotationSpeed);
		if(!IsAtEdge ())
		{
			this.transform.MoveFoward(this.transform.GetComponent<EnemyInfo>().movementSpeed);
		}
		foreach(Transform weapon in this.transform.GetComponentsInChildren<Transform>().Where(t => t.name == "BulletSpawnPoint"))
		{
			//rotate weapon and shoot
			this.transform.GetComponent<EnemyController>().ShootWeapons();
		}
		if(elapsedTime > duration)
		{
			this.transform.GetComponentInChildren<EnemyInfo>().isHit = false;
		}
	}
}

