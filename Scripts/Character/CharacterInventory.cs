using System;
using System.Collections;
using System.Collections.Generic;
using Game.UI;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class CharacterInventory
{
    public List<InventoryItem> Items = new();

    public void AddItem(InventoryItem item)
    {
        Items.Add(item);
        UI_Manager.Instance.AddInventoryItem(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        Items.Remove(item);
        UI_Manager.Instance.RemoveInventoryItem(item);
    }

    public void Refresh()
    {
        foreach (var item in Items)
        {
            UI_Manager.Instance.AddInventoryItem(item);
        }
    }
}

[Serializable]
public class InventoryItem
{
    public Sprite Icon;
    public string DisplayName;
    public string id;
    public string Description;
}
