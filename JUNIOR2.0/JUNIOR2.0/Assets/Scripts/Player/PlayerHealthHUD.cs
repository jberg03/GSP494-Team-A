using UnityEngine;
using System.Collections;

public class PlayerHealthHUD : MonoBehaviour {

    protected float maxHealth = 100;
	protected float currentHealth = 0;

    protected PlayerInfo playerHealth;

    public float healthBarLength;

    public Rect guiPos;
    //public Texture texture;

	// Use this for initialization
	void Start () {
        healthBarLength = Screen.width / 2;
        playerHealth = this.transform.GetComponent<PlayerInfo>();
		maxHealth = playerHealth.MaxHealth;
		currentHealth = playerHealth.health;
	}
	
	// Update is called once per frame
	void Update () {
        AdjustCurrentHealth(playerHealth.health);  
	}

    void OnGUI(){
        guiPos.Set(10, 10, healthBarLength, 20);

        if (playerHealth.GetPercentHealth() >= 65.0f)
            GUI.color = Color.green;
		else if (playerHealth.GetPercentHealth() > 35.0f)
            GUI.color = Color.red;
        else
            GUI.color = Color.yellow;

        GUI.Box(guiPos, currentHealth + "/" + maxHealth);
        
        //GUI.DrawTexture(guiPos, texture);
    }

    public void AdjustCurrentHealth(float adj)
	{
        currentHealth = adj;

        if (currentHealth < 0)
		{
            currentHealth = 0;
            Application.LoadLevel("GameOver");
		}
        if (currentHealth > maxHealth)
		{
            currentHealth = maxHealth;
		}
        if (maxHealth < 1)
		{
            maxHealth = 1;
		}
        healthBarLength = (Screen.width / 2) * (currentHealth / (float)maxHealth);
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.ToLower() == "bullet")
		{
			this.playerHealth.health -= other.GetComponent<Bullet>().damage;
			Destroy(other.gameObject);
		}
	}

    void Die()
    {
        MonoBehaviour p;
        Component[] coms = GetComponentsInChildren<MonoBehaviour>();
        foreach (Component b in coms)
        {
            p = b as MonoBehaviour;
            if (p)
                p.enabled = false;
        }
    }
}
