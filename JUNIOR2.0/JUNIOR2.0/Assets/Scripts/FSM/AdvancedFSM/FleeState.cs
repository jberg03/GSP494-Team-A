using UnityEngine;
using System.Collections;

public class FleeState : FSMState
{
	//protected FSMStateId stateId = FSMStateId.Flee;
	// Use this for initialization
	public override void Construct ()
	{
		this.stateId = FSMStateId.Flee;
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
		this.transform.RotateYAway(player, this.GetComponent<EnemyInfo>().rotationSpeed);
		this.transform.MoveBack(this.transform.GetComponent<PlayerInfo>().movementSpeed);
		CheckCornered();
	}

	private void CheckCornered()
	{
		if(IsAtEdge())
		{
			this.transform.GetComponent<EnemyController>().SetTransition(this.transform.GetComponent<AttackState>().RequiredTransition);
			this.transform.GetComponent<EnemyController>().RemoveState(this);
		}
	}


}

