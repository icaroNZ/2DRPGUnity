using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockbackDuration = 0.1f;
    private Rigidbody2D _rigidbody2D;
    public bool gettingKnockedBack { get; set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void GetKnockedBack(Transform damageSource, float force)
    {
        gettingKnockedBack = true;
        Vector3 direction = (transform.position - damageSource.position).normalized;
        _rigidbody2D.AddForce(direction * force * _rigidbody2D.mass, ForceMode2D.Impulse);
        StartCoroutine(KnockbackRoutine());
    }

    private IEnumerator KnockbackRoutine()
    {
        yield return new WaitForSeconds(knockbackDuration);
        gettingKnockedBack = false;
        _rigidbody2D.velocity = Vector3.zero;
    }
}
