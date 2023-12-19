using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Interactable : MonoBehaviour
{
    public InteractAction InteractAction => _interactAction;
    
    [SerializeReference]
    private InteractAction _interactAction;
    [SerializeField]
    private bool _destroyOnInteract = false;

    public string GetInteractionText() => _interactAction?.GetInteractText();

    public void Interact(Character character)
    {
        _interactAction.Interact(character);
        if(_destroyOnInteract)
            Destroy(this.gameObject);
    }

    [ContextMenu("Make Pickup")]
    private void MakePickup()
    {
        _interactAction = new PickupAction();
    }
    
    [ContextMenu("Make Level Transition")]
    private void MakeLevelTransition()
    {
        _interactAction = new LevelTransitionAction();
    }
    
    [ContextMenu("Remove Action")]
    private void RemoveAction()
    {
        _interactAction = null;
    }
}

[Serializable]
public abstract class InteractAction
{
    public abstract void Interact(Character character);
    public abstract string GetInteractText();
} 
