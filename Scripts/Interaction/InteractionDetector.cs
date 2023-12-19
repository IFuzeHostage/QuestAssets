using System;
using System.Collections;
using System.Collections.Generic;
using Game.UI;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    public Interactable CurrentInteractable => _currentInteractable;
    private Interactable _currentInteractable;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Interactable>(out var interactable))
        {
            _currentInteractable = interactable;
            UI_Manager.Instance.ShowInfoPanel(_currentInteractable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Interactable>(out var interactable) && interactable == _currentInteractable)
        {
            _currentInteractable = null;
            UI_Manager.Instance.HideInfoPanel();
        }
    }
}
