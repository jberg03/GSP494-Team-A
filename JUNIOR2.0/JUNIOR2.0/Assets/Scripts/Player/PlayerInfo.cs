using UnityEngine;
using System.Collections;

public class PlayerInfo : CharacterInfo
{
	public float energy;
	protected float maxEnergy;
	public float MaxEnergy{ get { return maxEnergy; } set { maxEnergy = value; } }
}

