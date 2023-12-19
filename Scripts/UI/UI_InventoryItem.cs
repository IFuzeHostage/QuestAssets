using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UI_InventoryItem : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        public void SetItem(InventoryItem item)
        {
            _icon.sprite = item.Icon;
        }
    }
}