using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AttackState : FSMState
{
	//protected FSMStateId stateId = FSMStateId.Attack;
	public override void Construct ()
	{
		this.stateId = FSMStateId.Attack;
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
		this.transform.RotateToward (player, this.GetComponent<EnemyInfo>().rotationSpeed);

		foreach(Transform weapon in this.transform.GetComponentsInChildren<Transform>().Where(t => t.name == "BulletSpawnPoint"))
		{
			//rotate weapon and shoot
			this.transform.GetComponent<EnemyController>().ShootWeapons();
		}
	}


}

