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
	BeenHit,
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
	HealthRegen = 0,
	EnergyRegen = 1,
	None = 2,
	Ammo = 3,
}

public enum FSMStateId
{
	None = 0,
	Patrol,
	Chase,
	Attack,
	Hit,
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

	
