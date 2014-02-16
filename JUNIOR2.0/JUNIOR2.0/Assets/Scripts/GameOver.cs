using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    public Texture gameOverTexture;

    void OnStart()
    {
        gameOverTexture = new Texture();
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2 - 300, 30, 600, Screen.height - 60), gameOverTexture, GUIStyle.none);

        if (GUI.Button(new Rect(Screen.width / 2 - 75, 300, 150, 75), "REPLAY"))
            Application.LoadLevel("Level01");
        if (GUI.Button(new Rect(Screen.width / 2 - 75, 390, 150, 75), "MAIN MENU"))
            Application.LoadLevel("MainMenu");
    }
}
