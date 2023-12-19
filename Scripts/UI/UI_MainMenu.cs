using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    public const string LEVEL_NAME = "main_menu";

    [SerializeField]
    private Button _newGameButton;
    [SerializeField]
    private Button _loadGameButton;

    private void Awake()
    {
        _newGameButton.onClick.AddListener(NewGame);
        _loadGameButton.onClick.AddListener(LoadGame);
    }

    private void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(LevelSaver.BASE_LEVEL_NAME);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(LevelSaver.BASE_LEVEL_NAME);
    }
}
