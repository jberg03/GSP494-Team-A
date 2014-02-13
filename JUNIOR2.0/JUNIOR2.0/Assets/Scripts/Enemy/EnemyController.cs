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
		if(currentState != null)
		{
			currentState.CheckState (playerTransform);
		}
	}

	protected override void FSMUpdate ()
	{
		//status update
		if(this.transform.GetComponent<EnemyInfo>().IsDead())
		{
			Destroy(Instantiate(this.transform.GetComponent<EnemyInfo>().deathEffect, this.transform.position, this.transform.rotation),3.0f);
			Destroy (gameObject);
		}
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

