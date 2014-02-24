using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class LootTable : MonoBehaviour
{
	public GameObject loot;

	public BaseUpgrade GetLoot()
	{
		int enumSize = Enum.GetValues(typeof(UpgradeId)).Length;
		int rndLootIndex = UnityEngine.Random.Range (0, enumSize);
		UpgradeId loot = (UpgradeId)rndLootIndex;
		IList<BaseUpgrade> upgrades = this.GetComponents<BaseUpgrade> ().Where (upgrade => upgrade.Id == loot).ToList();
		int rndUpgradeIndex = UnityEngine.Random.Range (0, upgrades.Count - 1);
		return upgrades.FirstOrDefault();
	}

	public void dropLoot(Transform target)
	{
		BaseUpgrade effect = this.GetLoot ();
		if(effect != null)
		{
			GameObject dropLoot = (GameObject)Instantiate (loot, target.position, target.rotation);
			dropLoot.tag = "Pickup";
			dropLoot.GetComponent<Loot> ().SetEffect(effect);
		}
	}
	
}

