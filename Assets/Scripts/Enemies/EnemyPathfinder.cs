using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinder : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;
    private Knockback _knockback;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _knockback = GetComponent<Knockback>();
    }

    private void FixedUpdate()
    {
        if (!_knockback.gettingKnockedBack)
        {
            Move();
        }
    }

    private void Move()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDirection = targetPosition;
    }

}
