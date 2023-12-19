using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = FILE_NAME, menuName = "Data/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public static ItemDatabase Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<ItemDatabase>(FILE_NAME);
            return _instance;
        }
    }
    private const string FILE_NAME = "so_itemDatabase";
    private static ItemDatabase _instance;

    [SerializeField]
    private List<ItemData> _items;

    public static ItemData GetItem(string itemId)
    {
        return Instance._items.FirstOrDefault(item => item.Item.id == itemId);
    }
}
