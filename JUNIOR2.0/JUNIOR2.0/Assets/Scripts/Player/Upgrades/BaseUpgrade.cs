using UnityEngine;
using System.Collections;

public abstract class BaseUpgrade : MonoBehaviour 
{
	protected UpgradeId id;
	protected float lifeTime = 0.0f;	
	public float duration = 15.0f;

	public UpgradeId Id
	{
		get{return id;}
	}

	void Update()
	{
		lifeTime += Time.deltaTime;
	}

	public abstract void Act(GameObject target);

	public bool IsComplete()
	{
		if(lifeTime > duration)
		{
			return true;
		}
		return false;
	}
}
