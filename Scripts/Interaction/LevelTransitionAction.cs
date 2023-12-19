using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionAction : InteractAction
{
    public string SceneName => _sceneName;
    
    [SerializeField]
    private string _sceneName;

    private const string INTERACT_TEXT = "Move to {0}";
    
    public override void Interact(Character character)
    {
        LevelSaver.LoadScene(_sceneName);
    }

    public override string GetInteractText()
    {
        return string.Format(INTERACT_TEXT, _sceneName);
    }
}
