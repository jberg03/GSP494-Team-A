using UnityEngine;
using System.Collections;

public class DeadState : FSMState
{

	public AudioClip sound;
	public GameObject effect;

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
		Destroy (gameObject);

	}
}

