using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicChanger : MonoBehaviour
{
    private string mainSceneName = "GameScene";
    private string gameSceneBackgroundMusicName = "Background";
    private string mainMenuSceneName = "MainMenuScene";
    private string mainMenuBackgroundMusicName = "MainMenuMusic";

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += ChangeBackgroundMusic;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= ChangeBackgroundMusic;
    }

    private void ChangeBackgroundMusic(Scene lastScene, Scene currentScene)
    {
        if (currentScene.name == mainSceneName)
        {
            AudioManager.Instance?.PlayBackgroundMusic(gameSceneBackgroundMusicName);
        }
        else if (currentScene.name == mainMenuSceneName) 
        {
            AudioManager.Instance?.PlayBackgroundMusic(mainMenuBackgroundMusicName);
        }
    }
}
