using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class LootTable : MonoBehaviour
{

	public BaseUpgrade GetLoot()
	{
		int enumSize = Enum.GetValues (UpgradeId).Length;
		int rndLootIndex = Random.Range (0, enumSize - 1);
		UpgradeId loot = rndLootIndex;
		BaseUpgrade[] upgrades = this.GetComponents<BaseUpgrade> ().Where (upgrade => upgrade.Id == loot);
		int rndUpgradeIndex = Random.Range (0, upgrades.Length - 1);
		return upgrades[rndUpgradeIndex];
	}
}

