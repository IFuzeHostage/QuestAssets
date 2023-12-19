using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class Character : ISaved
{
    public override string Id
    {
        get => _id;
    }
    
    public CharacterInventory Inventory => _inventory;
    
    public Action<Vector2> OnMove;
    public Action OnPickup;
    public bool CanMove = true;

    [SerializeField]
    private string _id;
    [SerializeField, Range(0f, 5f)]
    private float _speed;
    [SerializeField]
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private InteractionDetector _interaction;

    private CharacterInventory _inventory = new();
    private Vector2 _direction;

    public void Move(Vector2 direction)
    {
        if (!CanMove)
        {
            _rigidBody.velocity = Vector2.zero;
            return;
        }
        _rigidBody.velocity = direction.normalized * _speed;
        OnMove?.Invoke(direction);
    }

    public void Interaction()
    {
        _interaction?.CurrentInteractable?.Interact(this);
        OnPickup?.Invoke();
    }

    public override string Serialize()
    {
        var data = new CharacterSaveData()
        {
            InventoryIds = _inventory.Items.Select(item => item.id).ToList()
        };
        return JsonConvert.SerializeObject(data);
    }

    public override void Deserialize(string serializedData)
    {
        var data = JsonConvert.DeserializeObject<CharacterSaveData>(serializedData);
        if(ReferenceEquals(data, null))
            return;

        foreach (var id in data.InventoryIds)
        {
            _inventory.AddItem(ItemDatabase.GetItem(id).Item);   
        }
    }

    public override void Remove()
    {
        Destroy(gameObject);
    }

    [Serializable]
    private class CharacterSaveData
    {
        public List<string> InventoryIds;
    }
}
