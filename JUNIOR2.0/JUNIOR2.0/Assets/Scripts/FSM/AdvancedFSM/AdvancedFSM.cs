using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class AdvancedFSM : FSM
{
	protected List<FSMState> fsmStates;
	protected FSMStateId currentStateId;
	public FSMStateId CurrentStateId
	{
		get{return currentStateId;}
	}
	protected FSMState currentState;
	public FSMState CurrentState
	{
		get{return currentState;}
	}

	public AdvancedFSM()
	{
		fsmStates = new List<FSMState> ();
	}

	public void AddFSMState(FSMState fsmState)
	{
		if(fsmState != null)
		{
			if(fsmStates.Count == 0)
			{
				fsmStates.Add(fsmState);
				currentState = fsmState;
				currentStateId = fsmState.ID;
			}
			else
			{
				foreach(FSMState state in fsmStates)
				{
					if(state.ID == fsmState.ID)
					{
						Debug.LogWarning("The FSM State was already in the list");
						return;
					}
				}
				fsmStates.Add(fsmState);
			}
		}
		else
		{
			Debug.LogError("The FSM State is null");
		}
	}

	public void DeleteState(FSMStateId fsmState)
	{
		if(fsmState == FSMStateId.None)
		{
			return;
		}
		foreach(FSMState state in fsmStates)
		{
			if(state.ID == fsmState)
			{
				fsmStates.Remove(state);
				return;
			}
		}
		Debug.LogWarning ("The state id was not in the list");
	}

	public void PerformTransition(Transition transition)
	{
		if(transition == Transition.None)
		{
			Debug.LogWarning("The transition can not be null");
			return;
		}
		FSMStateId id = currentState.GetOutputState (transition);
		if(id == FSMStateId.None)
		{
			Debug.LogWarning("Current state does not have a target state");
			return;
		}
		currentStateId = id;
		foreach(FSMState state in fsmStates)
		{
			if(state.ID == currentStateId)
			{
				currentState = state;
				break;
			}
		}
	}
}

