using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSaver : MonoBehaviour
{
    public const string BASE_LEVEL_NAME = "level_1";
    public const string CHARACTER_PREFS_KEY = "character";
    public static string LAST_LEVEL = "";
    public static LevelSaver Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<LevelSaver>();
            return _instance;
        }
    }
    private static LevelSaver _instance;

    [SerializeField]
    private Character _playerCharacter;
    [SerializeReference]
    private List<ISaved> _levelObjects;
    [SerializeReference]
    private List<Interactable> _transitionInteractions;
    
    public static void LoadScene(string sceneName)
    {
        LAST_LEVEL = SceneManager.GetActiveScene().name;
        Instance.SaveState();
        SceneManager.LoadScene(sceneName);
    }
    
    public void SaveState()
    {
        LevelSaveData data = new();
        data.DataDict = new();
        foreach (var obj in _levelObjects)
        {
            if(obj == null)
                continue;
            
            data.DataDict[obj.Id] = obj.Serialize();
        }

        var levelName = SceneManager.GetActiveScene().name;
        var serializedLevel = JsonConvert.SerializeObject(data);
        var serializedCharacter = _playerCharacter.Serialize();
        PlayerPrefs.SetString(CHARACTER_PREFS_KEY, serializedCharacter);
        PlayerPrefs.SetString(levelName, serializedLevel);
        PlayerPrefs.Save();
    }

    public void LoadState()
    {
        TreDeserializeCharacter();
        
        var levelName = SceneManager.GetActiveScene().name;
        var serializedData = PlayerPrefs.GetString(levelName);
        if(ReferenceEquals(serializedData, null))
            return;

        var data = JsonConvert.DeserializeObject<LevelSaveData>(serializedData);
        if(ReferenceEquals(data, null))
            return;

        foreach (var obj in _levelObjects)
        {
            if (!data.DataDict.TryGetValue(obj.Id, out var objData))
            {
                obj.Remove();
                continue;
            }

            obj.Deserialize(objData);
        }
        
    }

    private void TreDeserializeCharacter()
    {
        if(_playerCharacter == null)
            return;
        
        var serializedData = PlayerPrefs.GetString(CHARACTER_PREFS_KEY);
        if(ReferenceEquals(serializedData, null))
            return;
        _playerCharacter.Deserialize(serializedData);
        
        if(_transitionInteractions.Count == 0)
            return;

        foreach (var interactable in _transitionInteractions)
        {
            var transition = interactable.InteractAction as LevelTransitionAction;
            if (transition == null || transition.SceneName != LAST_LEVEL)
                continue;

            _playerCharacter.transform.position = interactable.transform.position;
        }
    }
    
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        LoadState();
    }

    [Serializable]
    private class LevelSaveData
    {
        public Dictionary<string, string> DataDict;
    }
}
