using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;
    
    [SerializeField] private float moveSpeed = 5f;
    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public bool facingLeft { get; set; }

    private void Awake()
    {
        Instance = this;
        _playerControls = new PlayerControls();
        _rigidbody2D = GetComponent <Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        PlayerInput();
    }
    
    private void FixedUpdate()
    {
        AdjustPlayingFacingDirection();
        Move();
    }

    private void AdjustPlayingFacingDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        
        if (mousePosition.x < playerPosition.x)
        {
            _spriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
            facingLeft = false;
        }
        
    }

    private void Move()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        _animator.SetFloat("moveX", _movement.x);
        _animator.SetFloat("moveY", _movement.y);
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
    
}
