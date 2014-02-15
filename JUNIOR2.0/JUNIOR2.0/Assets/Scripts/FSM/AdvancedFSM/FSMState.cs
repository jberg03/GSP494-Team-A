using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public abstract class FSMState : MonoBehaviour
{
	protected Dictionary<Transition, FSMStateId> map = new Dictionary<Transition, FSMStateId>();
	protected FSMStateId stateId;
	//public static FSMStateId stateId;
	public FSMStateId ID 
	{
		get
		{
			return stateId;
		}
	}
	public Transition requiredTransition;
	public Transition RequiredTransition
	{
		get
		{
			return requiredTransition;
		}
	}
	protected Vector3 destPos;

	protected float minX;
	protected float minZ;
	protected float maxX;
	protected float maxZ;

	protected void DetermineBoundries()
	{
		Vector3 size = this.transform.GetComponent<EnemyInfo> ().platform.renderer.bounds.size;
		Vector3 position = this.transform.GetComponent<EnemyInfo> ().platform.transform.position;
		minX = position.x - (size.x / 2) + 2.0f;
		maxX = position.x + (size.x / 2) - 2.0f;
		minZ = position.z - (size.z / 2) + 2.0f;
		maxZ = position.z + (size.z / 2) - 2.0f;
	}

	public void AddTransition(Transition transition, FSMStateId id)
	{
		//make sure there are actual values passed in
		if( transition == Transition.None || id == FSMStateId.None)
		{
			Debug.LogWarning("No null FSM State or Transaction");
			return;
		}

		if(map.ContainsKey(transition))
		{
			Debug.LogWarning("transition is already used");
			return;
		}

		map.Add (transition, id);
	}

	public void DeleteTransition(Transition transition)
	{
		if(transition == Transition.None)
		{
			Debug.LogWarning("Transition can not be null");
			return;
		}

		if(map.ContainsKey(transition))
		{
			map.Remove(transition);
			return;
		}
		Debug.LogWarning ("The transistion is not used");
	}

	public FSMStateId GetOutputState(Transition transition)
	{
		if(transition == Transition.None)
		{
			Debug.LogError("Transition can not be null");
			return FSMStateId.None;
		}
		if(map.ContainsKey(transition))
		{
			return map[transition];
		}

		Debug.LogError ("Transition: " + transition + " was not in the list");
		return FSMStateId.None;
	}

	public void CheckState (Transform player)
	{
		float distance = Vector3.Distance (this.transform.position, player.position);
		Transition temp = Transition.None;
		EnemyInfo self = this.transform.GetComponent<EnemyInfo> ();
		bool deathState = map.ContainsKey (Transition.NoHealth);
		bool isDead = self.IsDead ();
		//check health
		if(self.IsDead() && (map.ContainsKey(Transition.NoHealth) || requiredTransition == Transition.NoHealth))
		{
			Debug.Log("Enemy is dead");
			temp = Transition.NoHealth;
		}
		else if(self.IsLowHealth() && (map.ContainsKey(Transition.LowHealth) || requiredTransition == Transition.LowHealth))
		{
			temp = Transition.LowHealth;
		}
		else if(distance <= self.reachTarget && (map.ContainsKey(Transition.ReachPlayer) || requiredTransition == Transition.ReachPlayer))
		{
			temp = Transition.ReachPlayer;
		}
		else if(distance <= self.seeTarget && (map.ContainsKey(Transition.SawPlayer) || requiredTransition == Transition.SawPlayer))
		{
			temp = Transition.SawPlayer;
		}
		else if((map.ContainsKey(Transition.LostPlayer) || requiredTransition == Transition.LostPlayer))
		{
			temp = Transition.LostPlayer;
		}
		else
		{
			temp = Transition.confused;
			//temp = this.requiredTransition;
		}
		
		//change states if the state is in the map
		if(temp != this.requiredTransition)
		{
			this.transform.GetComponent<EnemyController>().SetTransition(temp);
		}
	}

	public void CreateTransitions()
	{
		foreach(FSMState state in this.transform.GetComponent<EnemyController>().GetStates())
		{
			if(state.ID != this.stateId)
			{
				map.Add(state.requiredTransition, state.ID);
			}
		}
	}

	protected bool IsAtEdge()
	{

		Heading myDirection = this.transform.GetDirection ();
		float travelDistance = this.transform.GetComponent<EnemyInfo>().movementSpeed * Time.deltaTime;
		float x = transform.position.x;
		float z = transform.position.z;
		switch(myDirection)
		{
		case Heading.north:
			if(z - travelDistance < minZ) 
			{
				return true;
			}
			break;
		case Heading.northeast:
			if((x - travelDistance < minX) || (z - travelDistance < minZ)) 
			{
				return true;
			}
			break;
		case Heading.east:
			if(x - travelDistance < minX)
			{
				return true;
			}
			break;
		case Heading.southeast:
			if((z + travelDistance > maxZ) || (x - travelDistance < minX))
			{
				return true;
			}
			break;
		case Heading.south:
			if(z + travelDistance > maxZ)
			{
				return true;
			}
			break;
		case Heading.southwest:
			if((x + travelDistance > maxX) || (z + travelDistance > maxZ))
			{
				return true;
			}
			break;
		case Heading.west:
			if(x + travelDistance > maxX)
			{
				return true;
			}
			break;
		case Heading.northwest:
			if((z - travelDistance < minZ) || (x + travelDistance > maxX))
			{
				return true;
			}
			break;
		}
		return false;
	}

	public abstract void Act (Transform player);
	public abstract void Construct();
}

