using UnityEngine;
using System.Collections;

public class CharacterInfo : MonoBehaviour 
{
	public float health = 30;
	protected float maxHealth;
	public float movementSpeed = 3.0f;
	public float rotationSpeed = 1.0f;
	protected StatusCondition condition;
	public float MaxHealth{get{return maxHealth;}set{ maxHealth = value; }}

	void Start()
	{
		maxHealth = health;
	}

	public bool IsLowHealth()
	{
		if(((health / maxHealth) * 100) > 20)
		{
			return false;
		}
		return true;
	}

	public float GetMovementSpeed()
	{
		//check status
		return movementSpeed;
	}
	
	public float GetRotationSpeed()
	{
		return rotationSpeed;
	}
	
	public void DealDamage(float damage)
	{
		health -= damage;
	}
	
	public bool IsDead()
	{
		if(health <= 0)
		{
			return true;
		}
		return false;
	}
	
	public void ApplyStatus(StatusCondition statCondition)
	{
		if(statCondition != null)
		{
			
		}
	}
}
