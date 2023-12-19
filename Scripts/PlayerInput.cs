using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Character _movement;

    private PlayerControls _controller;

    private bool _moveActive;
    
    private void MoveOnPerformed(InputAction.CallbackContext obj) => _moveActive = true;

    private void MoveOnCancelled(InputAction.CallbackContext obj)
    {
        _moveActive = false;
        _movement.Move(Vector2.zero);
    }
    
    private void InteractionOnperformed(InputAction.CallbackContext obj)
    {
        _movement.Interaction();
    }
    
    private void OnEnable()
    {
        _controller.Enable();;
        _controller.Player.Move.performed += MoveOnPerformed;
        _controller.Player.Move.canceled += MoveOnCancelled;
        _controller.Player.Interaction.performed += InteractionOnperformed;
    }


    private void OnDisable()
    {
        _controller.Disable();
        _controller.Player.Move.performed -= MoveOnPerformed;
        _controller.Player.Move.canceled -= MoveOnCancelled;
    }


    private void Awake()
    {
        _controller = new();
    }

    private void Update()
    {
        if (_moveActive)
        {
            _movement.Move(_controller.Player.Move.ReadValue<Vector2>());
        }   
    }
}
