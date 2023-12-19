using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField]
    private ItemData _data;
    
    public void SetPickupData(ItemData data)
    {
        _data = data;
        _spriteRenderer.sprite = data.Item.Icon;
    }

    private void Awake()
    {
        if(_data)
            SetPickupData(_data);
    }
}

[Serializable]
public class PickupAction : InteractAction
{
    [SerializeField]
    private ItemData _data;
    
    public override void Interact(Character character)
    {
        character.Inventory.AddItem(_data.Item);
    }

    public override string GetInteractText()
    {
        return _data.Item.DisplayName;
    }
}
