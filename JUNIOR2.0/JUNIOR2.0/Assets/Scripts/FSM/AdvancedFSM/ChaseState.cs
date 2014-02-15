using UnityEngine;
using System.Collections; 

public class ChaseState : FSMState
{
	// Use this for initialization
	public override void Construct()
	{
		this.stateId = FSMStateId.Chase;
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
		this.transform.RotateYToward (player, this.GetComponent<EnemyInfo>().rotationSpeed);
		if(!IsAtEdge ())
		{
			this.transform.MoveBack(this.transform.GetComponent<EnemyInfo>().movementSpeed);
		}
	}
}

