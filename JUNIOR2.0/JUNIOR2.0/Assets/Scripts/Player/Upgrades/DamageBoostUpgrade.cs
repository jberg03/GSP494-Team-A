using UnityEngine;
using System.Collections;

public class DamageBoostUpgrade : BaseUpgrade
{
	public GameObject bullet;
	protected GameObject originalbullet = null;
	protected float elapsedTime = 0.0f;

	void Start()
	{
		id = UpgradeId.Ammo;
	}

	public override void Act (GameObject target)
	{
		elapsedTime += Time.deltaTime;
		lifeTime += Time.deltaTime;
		if(originalbullet == null)
		{
			originalbullet = target.transform.GetComponentInChildren<Weapon> ().bullet;
			target.transform.GetComponentInChildren<Weapon> ().bullet = bullet;
		}
		if(elapsedTime > duration)
		{
			target.transform.GetComponentInChildren<Weapon>().bullet = this.originalbullet;
		}
	}
}

