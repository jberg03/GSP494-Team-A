using UnityEngine;
using System.Collections;

public class StationaryState : FSMState
{

	public override void Construct ()
	{
		this.stateId = FSMStateId.Stationary;
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
		this.transform.rotation = new Quaternion (0.0f, this.transform.rotation.y, 0.0f, this.transform.rotation.w);
	}
}

