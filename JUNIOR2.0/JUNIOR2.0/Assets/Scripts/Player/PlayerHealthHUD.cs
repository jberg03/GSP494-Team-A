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
	}
	
	// Update is called once per frame
	void Update () {
        AdjustCurrentHealth(playerHealth.health);  
	}

    void OnGUI(){
        guiPos.Set(10, 10, healthBarLength, 20);
		GUI.backgroundColor = Color.gray;

        if (currentHealth >= 65)
            GUI.color = Color.green;
        else if (currentHealth >= 35 && currentHealth < 65)
            GUI.color = Color.yellow;
        else
            GUI.color = Color.red;

        GUI.Box(guiPos, currentHealth + "/" + maxHealth);
        
        //GUI.DrawTexture(guiPos, texture);
    }

    public void AdjustCurrentHealth(float adj){
        currentHealth = adj;

        if (currentHealth < 0)
            currentHealth = 0;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (maxHealth < 1)
            maxHealth = 1;

        healthBarLength = (Screen.width / 2) * (currentHealth / (float)maxHealth);
    }
}
