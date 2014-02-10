using UnityEngine;
using System.Collections;

public class StationaryState : FSMState
{

	public override void Construct ()
	{
		this.stateId = FSMStateId.Stationary;
		this.DetermineBoundries ();
	}

	public override void Act (Transform player)
	{

	}
}

