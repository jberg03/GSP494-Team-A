using UnityEngine;
using System.Collections;

public class HealthRegenUpgrade : BaseUpgrade
{
	public float regenAmount = 3.0f;
	public float regenRate = 0.5f;
	private float elapsedTime = 0.0f;


	void Start()
	{
		id = UpgradeId.HealthRegen;
	}

	void Update()
	{
		elapsedTime += Time.deltaTime;
	}

	public override void Act (GameObject target)
	{
		if(target.tag.ToLower().Equals("player"))
		{
			if(elapsedTime >= regenRate)
			{
				target.GetComponent<PlayerInfo>().GainHealth(regenAmount);
				elapsedTime = 0.0f;
			}
		}
	}
}

