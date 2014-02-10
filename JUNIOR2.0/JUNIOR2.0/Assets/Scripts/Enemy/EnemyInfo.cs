using UnityEngine;
using System.Collections;



public class EnemyInfo : MonoBehaviour
{
	public float health = 30;
	protected float maxHealth;
	protected StatusCondition condition;
	public float reachTarget = 5.0f;
	public float seeTarget = 25.0f;
	public float movementSpeed = 3.0f;
	public float rotationSpeed = 1.0f;

	public GameObject platform;



	void Start()
	{
		maxHealth = health;
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

	public bool IsLowHealth()
	{
		if(((health / maxHealth) * 100) > 20)
		{
			return false;
		}
		return true;
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

