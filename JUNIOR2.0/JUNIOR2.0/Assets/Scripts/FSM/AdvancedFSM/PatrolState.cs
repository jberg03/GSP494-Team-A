using UnityEngine;
using System.Collections;

public class PatrolState : FSMState
{
	//protected FSMStateId stateId = FSMStateId.Patrol;
	public GameObject startingPoint;

	public override void Construct()
	{
		this.stateId = FSMStateId.Patrol;
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
		if(startingPoint != null)
		{
			this.transform.RotateYToward (startingPoint.transform, this.GetComponent<EnemyInfo>().rotationSpeed);
			this.transform.MoveBack (this.transform.GetComponent<EnemyInfo>().movementSpeed);
			float distance = Vector3.Distance (this.transform.position, startingPoint.transform.position);
			if(distance <= (int)Transition.ReachPlayer)
			{
				startingPoint = startingPoint.transform.GetComponent<Waypoint>().nextPoint;
			}
		}
		else
		{
			Debug.LogWarning("Couldn't find a waypoint");
			this.transform.GetComponent<EnemyController>().SetTransition(Transition.confused);
			this.transform.GetComponent<EnemyController>().RemoveState(this);
		}

	}
}

