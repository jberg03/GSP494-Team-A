using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerInfo : CharacterInfo
{
	public float energy = 30;
	protected float maxEnergy;
    public float MaxEnergy { get { if (maxEnergy == 0) { maxEnergy = energy; } return maxEnergy; } }
	protected IList<BaseUpgrade> upgrades;
	public BaseUpgrade[] upgradeList;

    void Start()
    {
        maxEnergy = energy;
		upgrades = new List<BaseUpgrade> ();
    }

    public bool isLowEnergy()
    {
        if (((energy / maxEnergy) * 100) > 30)
            return false;

        return true;
    }

    public float GetPercentEnergy()
    {
        return (energy / maxEnergy) * 100; 
    }

    public void loseEnergy(float amount)
    {
        energy -= amount;
    }

	public void GainEnergy(float amount)
	{
		energy += amount;
		if(energy > maxEnergy)
		{
			energy = maxEnergy;
		}
	}

    public bool isOutOfEnergy()
    {
        if (energy <= 0)
            return true;

        return false;
    }

	public void AddUpgrade(BaseUpgrade loot)
	{
		if(!upgrades.Contains(loot))
		{
			upgrades.Add (loot);
		}
	}

	public void RemoveUpgrade(BaseUpgrade loot)
	{
		if(upgrades.Contains(loot))
		{
			upgrades.Remove(loot);
		}
	}

	void FixedUpdate()
	{
		if(upgrades != null && upgrades.Count > 0)
		{
			IList<BaseUpgrade> removeList = new List<BaseUpgrade>();
			foreach(BaseUpgrade upgrade in upgrades)
			{
				upgrade.Act(this.gameObject);
				if(upgrade.IsComplete())
				{
					removeList.Add(upgrade);
				}
			}
			foreach(BaseUpgrade upgrade in removeList)
			{
				upgrades.Remove(upgrade);
			}
		}

	}

	void Update()
	{
		upgradeList = upgrades.ToArray ();
	}
}

