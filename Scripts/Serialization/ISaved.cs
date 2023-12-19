using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISaved : MonoBehaviour
{
    public abstract string Id { get; }

    public abstract string Serialize();
    public abstract void Deserialize(string data);
    public abstract void Remove();
}
