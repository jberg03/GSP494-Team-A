using UnityEngine;
using System.Collections;

public class EnergyRegenUpgrade : BaseUpgrade
{
	public float energyAmount = 1.0f;
	public float energyRate = 1.0f;
	protected float elapsedTime = 0.0f;
	// Use this for initialization
	void Start ()
	{
		id = UpgradeId.EnergyRegen;
	}



	public override void Act (GameObject target)
	{
		lifeTime += Time.deltaTime;
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= energyRate)
		{
			target.GetComponent<PlayerInfo>().GainEnergy(energyAmount);
			elapsedTime = 0.0f;
		}
	}
}

