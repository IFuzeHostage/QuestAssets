using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField]
    private Character _movement;
    [SerializeField]
    private Animator _animator;

    private const string HORIZONTAL_KEY = "horizontal";
    private const string VERTICAL_KEY = "vertical";
    private const string IS_MOVING_KEY = "is_moving";
    private const string PICKUP_KEY = "pickup";

    private void SetDirection(Vector2 direction)
    {
        _animator.SetFloat(HORIZONTAL_KEY, direction.x);
        _animator.SetFloat(VERTICAL_KEY, direction.y);
    }

    private void SetIsMoving(bool isMoving)
    {
        _animator.SetBool(IS_MOVING_KEY, isMoving);
    }

    private void TriggerPickup()
    {
        _animator.SetTrigger(PICKUP_KEY);
    }

    private void OnPickup()
    {
        LockMovement();
        TriggerPickup();
    }

    private void LockMovement() => _movement.CanMove = false;
    private void UnlockMovement() => _movement.CanMove = true;
    
    private void OnMove(Vector2 vec)
    {
        var isMoving = vec != Vector2.zero;
        SetIsMoving(isMoving);
        if(isMoving)
            SetDirection(vec);
    }
    
    private void Awake()
    {
        _movement.OnMove += OnMove;
        _movement.OnPickup += OnPickup;
    }

}
