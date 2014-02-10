using UnityEngine;
using System.Collections; 

public class ChaseState : FSMState
{
	// Use this for initialization
	public override void Construct()
	{
		this.stateId = FSMStateId.Chase;
		this.DetermineBoundries ();
	}

	public override void Act (Transform player)
	{
		this.transform.RotateYToward (player);
		if(!IsAtEdge ())
		{
			this.transform.MoveFoward();
		}
	}
}

