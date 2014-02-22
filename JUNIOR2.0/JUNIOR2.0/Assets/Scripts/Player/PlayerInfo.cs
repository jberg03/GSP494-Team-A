using UnityEngine;
using System.Collections;

public class PlayerInfo : CharacterInfo
{
	public float energy = 30;
	protected float maxEnergy;
    public float MaxEnergy { get { if (maxEnergy == 0) { maxEnergy = energy; } return maxEnergy; } }

    void Start()
    {
        maxEnergy = energy;
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

    public bool isOutOfEnergy()
    {
        if (energy <= 0)
            return true;

        return false;
    }
}

