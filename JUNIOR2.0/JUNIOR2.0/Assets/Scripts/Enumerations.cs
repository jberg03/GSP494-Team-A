using UnityEngine;
using System.Collections;

public enum Transition
{
	None,
	confused,
	SawPlayer,
	ReachPlayer,
	LostPlayer,
	NoHealth,
	LowHealth,
}

public enum PlatformDirection
{
	none,
	up,
	down,
	left,
	right,
	foward,
	backward,
}

public enum PlatformRotation
{
	none,
	clockwise,
	counterClockwise,
}

public enum UpgradeId
{
	HealthRegen = 3,
	EnergyRegen = 2,
	Ammo = 1,
	None = 0,
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

	
