using UnityEngine;
using System.Collections;

public class DeadState : FSMState
{
	public GameObject effect;
	public GameObject lootTable;
	public float energyRestore = 5.0f;

	public override void Construct ()
	{
		this.stateId = FSMStateId.Dead;
		if(this.transform.GetComponent<EnemyInfo>().platform != null)
		{
			this.DetermineBoundries ();
		}
	}

	void Update()
	{
		if(this.transform.GetComponent<EnemyInfo>().platform != null)
		{
			this.DetermineBoundries ();
		}
	}

	public override void Act (Transform player)
	{
		//status update
		Debug.Log ("Enemy is about to explode");
		//AudioSource.PlayClipAtPoint (sound, this.transform.position);
		Destroy(Instantiate(this.effect, this.transform.position, this.transform.rotation),7.0f);
		player.GetComponentInChildren<PlayerInfo> ().GainEnergy (energyRestore);
		Destroy (gameObject);

	}
}

