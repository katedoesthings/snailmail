using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public string gameLevel;

    public void Play() {
        SceneManager.LoadScene(gameLevel);
    }

    public void Quit() {
        Application.Quit();
    }
}
