using UnityEngine;
using System.Collections;

public class PlayerEnergyHUD : MonoBehaviour
{

    protected float maxEnergy = 100;
    protected float currentEnergy = 0;
    protected PlayerInfo playerEnergy;

    public float energyBarLength;

    public Rect guiPos;
    //public Texture texture;

    // Use this for initialization
    void Start()
    {
        energyBarLength = Screen.width / 2;
        playerEnergy = this.transform.GetComponent<PlayerInfo>();
        maxEnergy = playerEnergy.MaxEnergy;
        currentEnergy = playerEnergy.energy;
    }

    // Update is called once per frame
    void Update()
    {
        
        AdjustCurrentEnergy(playerEnergy.energy);
        Jumping();
        Moving();
    }

    void OnGUI()
    {
        guiPos.Set(10, 32, energyBarLength, 20);

        if (playerEnergy.GetPercentEnergy() >= 65.0f)
            GUI.color = Color.yellow;
        else if (playerEnergy.GetPercentEnergy() > 35.0f)
            GUI.color = Color.magenta;
        else
            GUI.color = Color.gray;

        GUI.Box(guiPos, (int)currentEnergy + "/" + maxEnergy);

        
        //GUI.DrawTexture(guiPos, texture);
    }

    public void AdjustCurrentEnergy(float adj)
    {
        currentEnergy = adj;

        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        if (maxEnergy < 1)
        {
            maxEnergy = 1;
        }
        energyBarLength = (Screen.width / 2) * (currentEnergy / (float)maxEnergy);
    }

    public void Jumping()
    {
        if (Input.GetButtonDown("Jump") && !playerEnergy.isOutOfEnergy())
            playerEnergy.loseEnergy(1.0f);
    }

    public void Moving()
    {
		if(!playerEnergy.isOutOfEnergy())
		{
	        if (Input.GetKey(KeyCode.W))
	            playerEnergy.loseEnergy(0.1f * Time.deltaTime);
	        if (Input.GetKey(KeyCode.A))
	            playerEnergy.loseEnergy(0.1f * Time.deltaTime);
	        if (Input.GetKey(KeyCode.S))
	            playerEnergy.loseEnergy(0.1f * Time.deltaTime);
	        if (Input.GetKey(KeyCode.D))
	            playerEnergy.loseEnergy(0.1f * Time.deltaTime);
		}
    }
}
