using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyController : AdvancedFSM
{
	protected override void Initialize ()
	{
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		ConstructFSM ();
	}

	protected override void FSMUpdate ()
	{

	}

	protected override void FSMFixedUpdate ()
	{
		if(currentState != null)
		{
			currentStateId = currentState.ID;
			CurrentState.Act (playerTransform);
			CurrentState.CheckState (playerTransform);
		}
	}

	public void SetTransition(Transition transition)
	{
		PerformTransition (transition);
	}

	private void ConstructFSM()
	{
		this.gameObject.AddComponent ("StationaryState");
		StationaryState stationaryState = this.GetComponent<StationaryState> ();
		stationaryState.requiredTransition = Transition.confused;
		foreach(FSMState state in this.transform.GetComponents<FSMState>())
		{
			state.Construct();
			fsmStates.Add(state);
		}
		foreach(FSMState state in fsmStates)
		{
			state.CreateTransitions();
			currentState = state;
			currentStateId = state.ID;
		}
		currentState.CheckState (playerTransform);

	}

	public void RemoveState(FSMState item)
	{
		this.DeleteState (item.ID);
		foreach(FSMState state in fsmStates)
		{
			state.DeleteTransition(item.RequiredTransition);
		}
	}

	public IList<FSMState> GetStates()
	{
		return this.fsmStates;
	}

	protected void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag.ToLower() == "bullet")
		{

			this.transform.GetComponent<EnemyInfo>().DealDamage(other.gameObject.transform.GetComponent<Bullet>().damage);
			this.transform.GetComponent<EnemyInfo>().ApplyStatus(other.gameObject.transform.GetComponent<Bullet>().GetCondition());
			Destroy (other.gameObject);
		}
		if(other.gameObject.tag.ToLower() == "platform"  && this.transform.GetComponent<EnemyInfo>().platform != other.gameObject)
		{
			this.transform.GetComponent<EnemyInfo>().platform = other.gameObject;
		}
	}

	protected void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.ToLower() == "bullet")
		{
			
			this.transform.GetComponent<EnemyInfo>().DealDamage(other.gameObject.transform.GetComponent<Bullet>().damage);
			this.transform.GetComponent<EnemyInfo>().ApplyStatus(other.gameObject.transform.GetComponent<Bullet>().GetCondition());
			Destroy(other.gameObject);
		}
	}

	public void ShootWeapons()
	{
		Weapon[] weapons = this.GetComponentsInChildren<Weapon> ();
		foreach(Weapon weapon in weapons)
		{
			//weapon.transform.RotateToward(playerTransform);
			weapon.Fire();
		}
	}
}

