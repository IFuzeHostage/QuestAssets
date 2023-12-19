using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SavedLevelObject : ISaved
{
    public override string Id => _itemId;
    [SerializeField]
    private string _itemId;

    public override string Serialize()
    {
        var data = new SaveData()
        {
            ItemId = _itemId,
            xPos = transform.position.x,
            yPos = transform.position.y
        };
        return JsonConvert.SerializeObject(data);
    }

    public override void Deserialize(string serializedData)
    {
        var data = JsonConvert.DeserializeObject<SaveData>(serializedData);
        if(data == null)
            return;

        var pos = new Vector2(data.xPos, data.yPos);
        transform.position = pos;
    }

    public override void Remove()
    {
        Destroy(gameObject);
    }

    protected class SaveData
    {
        public string ItemId;

        //Json has a hard time serializeing vectors
        public float xPos;
        public float yPos;
    }
    //item id 
    //Serialize
    //Deserialize
}
