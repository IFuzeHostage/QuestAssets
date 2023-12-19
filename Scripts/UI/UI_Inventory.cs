using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.UI
{
    public class UI_Inventory : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _itemParent;

        [SerializeField]
        private UI_InventoryItem _itemPrefab;

        private Dictionary<string, UI_InventoryItem> _items = new();

        public void AddInventoryItem(InventoryItem item)
        {
            if (_items.TryGetValue(item.id, out var uiItem))
            {
                uiItem.SetItem(item);
                return;
            }

            var newitem = Instantiate(_itemPrefab, _itemParent);
            newitem.SetItem(item);
            _items[item.id] = newitem;
        }

        public void RemoveInventoryItem(InventoryItem item)
        {
            if (_items.TryGetValue(item.id, out var uiItem))
            {
                Destroy(uiItem.gameObject);
                _items.Remove(item.id);
            }
        }
    }
}