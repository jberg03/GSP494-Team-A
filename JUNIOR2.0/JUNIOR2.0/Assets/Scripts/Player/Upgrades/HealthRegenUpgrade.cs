using UnityEngine;
using System.Collections;

public class HealthRegenUpgrade : BaseUpgrade
{
	public float regenAmount = 1.0f;
	public float regenRate = 3f;
	private float elapsedTime = 0.0f;


	void Start()
	{
		id = UpgradeId.HealthRegen;
	}
	
	public override void Act (GameObject target)
	{
		lifeTime += Time.deltaTime;
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= regenRate)
		{
			target.GetComponent<PlayerInfo>().GainHealth(regenAmount);
			elapsedTime = 0.0f;
		}
	}
}

