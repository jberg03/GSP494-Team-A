using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour
{
	public BaseUpgrade lootEffect;

	public void SetEffect(BaseUpgrade effect)
	{
		lootEffect = effect;
		this.ChangeGlow ();
	}

	protected void ChangeGlow()
	{
		ParticleSystem ps = this.transform.GetComponent<ParticleSystem> ();
		switch(lootEffect.Id)
		{
		case UpgradeId.EnergyRegen:
			ps.startColor = Color.yellow;
			break;
		case UpgradeId.HealthRegen:
			ps.startColor = Color.red;
			break;
		}
		
	}
}

