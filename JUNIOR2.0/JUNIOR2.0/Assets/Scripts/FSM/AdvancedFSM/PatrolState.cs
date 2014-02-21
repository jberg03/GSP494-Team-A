using UnityEngine;
using System.Collections;

public class PatrolState : FSMState
{
	//protected FSMStateId stateId = FSMStateId.Patrol;
	public Transform startingPoint;
	private GameObject platform;
	public float reachPoint = 10.0f;

	public override void Construct()
	{
		this.stateId = FSMStateId.Patrol;
		platform = this.transform.GetComponent<EnemyInfo> ().platform;
		if( platform != null)
		{
			this.DetermineBoundries ();
			startingPoint = platform.GetComponentInChildren<Waypoints>().GetFirstPoint();
		}
	}

	void Update()
	{
		if(this.transform.GetComponent<EnemyInfo>().platform != null )
		{
			this.DetermineBoundries ();
		}
	}

	public override void Act (Transform player)
	{
		if(startingPoint != null)
		{
			this.transform.RotateYToward (startingPoint.transform, this.GetComponent<EnemyInfo>().rotationSpeed);
			this.transform.MoveFoward (this.transform.GetComponent<EnemyInfo>().movementSpeed);
			float distance = Vector3.Distance (this.transform.position, startingPoint.transform.position);
			if(distance <= (int)reachPoint)
			{
				startingPoint = platform.GetComponentInChildren<Waypoints>().GetNextPoint(startingPoint);
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

