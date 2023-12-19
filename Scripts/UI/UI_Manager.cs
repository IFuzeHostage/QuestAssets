using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI
{
    public class UI_Manager : MonoBehaviour
    {
        public static UI_Manager Instance
        {
            get
            {
                if (!_instance)
                    _instance = FindObjectOfType<UI_Manager>();
                return _instance;
            }
        }

        private static UI_Manager _instance;

        [SerializeField]
        private UI_InfoPanel _infoPanel;

        [SerializeField]
        private UI_Inventory _inventory;

        [SerializeField]
        private Button _saveButton;
        [SerializeField]
        private Button _exitButton;
        
        public void AddInventoryItem(InventoryItem item)
        {
            _inventory.AddInventoryItem(item);
        }

        public void RemoveInventoryItem(InventoryItem item)
        {
            _inventory.RemoveInventoryItem(item);
        }

        public void ShowInfoPanel(Interactable interactable)
        {
            _infoPanel.gameObject.SetActive(true);
            _infoPanel.SetText(interactable.GetInteractionText());
            _infoPanel.SetFollowed(interactable.transform);
        }

        public void HideInfoPanel()
        {
            _infoPanel.gameObject.SetActive(false);
            _infoPanel.SetFollowed(null);
        }

        private void Awake()
        {
            _instance = this;
            _saveButton.onClick.AddListener(SaveData);
            _exitButton.onClick.AddListener(ExitToMenu);
        }

        private void ExitToMenu()
        {
            SceneManager.LoadScene(UI_MainMenu.LEVEL_NAME);
        }

        private void SaveData()
        {
            LevelSaver.Instance.SaveState();
        }
    }
}