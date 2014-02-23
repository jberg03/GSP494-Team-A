using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    
    public Texture gameOverTexture;
    public Texture background;

    void OnStart()
    {
        gameOverTexture = new Texture();
        background = new Texture();
    }

    void OnGUI()
    {
        Screen.showCursor = true;

        GUI.Box(new Rect(Screen.width / 2 - 300, 30, 600, Screen.height - 60), gameOverTexture, GUIStyle.none);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);

        if (GUI.Button(new Rect(Screen.width / 2 - 75, 300, 150, 75), "REPLAY"))
            Application.LoadLevel("Level01");
        if (GUI.Button(new Rect(Screen.width / 2 - 75, 390, 150, 75), "MAIN MENU"))
            Application.LoadLevel("MainMenu");
    }
}
