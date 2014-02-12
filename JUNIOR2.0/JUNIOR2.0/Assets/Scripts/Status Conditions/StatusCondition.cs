using UnityEngine;
using System.Collections;

public class StatusCondition : MonoBehaviour
{
	protected float elapsedTime;
	public float duration;
	protected StatusConditionId conditionId;

	public StatusConditionId ConditionId
	{
		get
		{
			return conditionId;
		}
	}

	public void UpdateCondition()
	{

	}


}

