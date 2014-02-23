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

	// Update is called once per frame
	void Update ()
	{
		elapsedTime += Time.deltaTime;
	}

	public override void Act (GameObject target)
	{
		if(target.tag.ToLower().Equals("player"))
		{

		}
	}
}

