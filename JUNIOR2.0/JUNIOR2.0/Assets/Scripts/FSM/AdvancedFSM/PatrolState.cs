using UnityEngine;
using System.Collections;

public class PatrolState : FSMState
{
	//protected FSMStateId stateId = FSMStateId.Patrol;
	public GameObject startingPoint;

	public override void Construct()
	{
		this.stateId = FSMStateId.Patrol;
		this.DetermineBoundries ();
	}

	public override void Act (Transform player)
	{
		this.transform.RotateYToward (startingPoint.transform);
		this.transform.MoveFoward ();
		float distance = Vector3.Distance (this.transform.position, startingPoint.transform.position);
		if(distance <= (int)Transition.ReachPlayer)
		{
			startingPoint = startingPoint.transform.GetComponent<Waypoint>().nextPoint;
		}

	}
}

