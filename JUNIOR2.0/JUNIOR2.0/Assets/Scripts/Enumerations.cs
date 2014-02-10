using UnityEngine;
using System.Collections;

public enum Transition
{
	None,
	SawPlayer,
	ReachPlayer,
	LostPlayer,
	NoHealth,
	LowHealth,
}

public enum FSMStateId
{
	None = 0,
	Patrol,
	Chase,
	Attack,
	Stationary,
	Flee,
	Dead,
}
public enum StatusConditionId
{
	None,
	Poisoned,
	Paralyzed,
	Burned,

}

public enum Heading
{
	north,
	northeast,
	east,
	southeast,
	south,
	southwest,
	west,
	northwest,
}

	
