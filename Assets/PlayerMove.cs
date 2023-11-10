using Cinemachine;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputSettings;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] InputActionReference _move;
    [SerializeField] float _speed;

    // Event pour les dev
    public event Action OnStartMove;
    public event Action<int> OnHealthUpdate;

    // Event pour les GD
    [SerializeField] UnityEvent _onEvent;
    [SerializeField] UnityEvent _onEventPost;

    [SerializeField] private Animator _animator;

    public Vector2 JoystickDirection { get; private set; }
    Coroutine MovementRoutine { get; set; }

    [SerializeField] private Rigidbody _rb;

    private void Start()
    {
        _move.action.started += StartMove;
        _move.action.performed += UpdateMove;
        _move.action.canceled += StopMove;

    }

    private void StartMove(InputAction.CallbackContext context)
    {
        OnStartMove?.Invoke();
        _animator.SetBool("Walking", true);
    }

    private void UpdateMove(InputAction.CallbackContext context)
    {
        _rb.velocity = new Vector3(_move.action.ReadValue<Vector2>().x * _speed, _rb.velocity.y, _move.action.ReadValue<Vector2>().y * _speed);
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        _animator.SetBool("Walking", false);
    }


    private void OnDestroy()
    {
        _move.action.started -= StartMove;
        _move.action.performed -= UpdateMove;
        _move.action.canceled -= StopMove;
    }

}
