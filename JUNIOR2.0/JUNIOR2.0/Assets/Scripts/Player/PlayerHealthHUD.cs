using UnityEngine;
using System.Collections;

public class PlayerHealthHUD : MonoBehaviour {

    public int maxHealth = 100;
    private int currentHealth = 0;

    PlayerHealth playerHealth;

    public float healthBarLength;

    public Rect guiPos;
    //public Texture texture;

	// Use this for initialization
	void Start () {
        healthBarLength = Screen.width / 2;
        playerHealth = GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        AdjustCurrentHealth(playerHealth.health);  
	}

    void OnGUI(){
        guiPos.Set(10, 10, healthBarLength, 20);
        GUI.Box(guiPos, currentHealth + "/" + maxHealth);
        //GUI.DrawTexture(guiPos, texture);
    }

    public void AdjustCurrentHealth(int adj){
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
