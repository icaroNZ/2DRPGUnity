using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class Sword : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    [SerializeField] private Transform _weaponCollider;
    [SerializeField] private float swordAttackCd = 0.5f;
    [SerializeField] private GameObject _slashAnimationPrefab;
    [SerializeField] private Transform _slashAnimationSpawnPoint;
    private GameObject _slashAnimation;
    private bool _attackButtonPressed, _isAttacking = false;
    

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _animator = GetComponent<Animator>();
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
    }
    void Start()
    {
        _playerControls.Combat.Attack.started += _ => StartAttacking();
        _playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        MouseFollowWithOffset(); 
        Attack();
    }
    
    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(swordAttackCd);
        _isAttacking = false;
    }

    private void Attack()
    {
        if (_attackButtonPressed && !_isAttacking)
        {
            _isAttacking = true;
            _weaponCollider.gameObject.SetActive(true);
            _slashAnimation = Instantiate(_slashAnimationPrefab, _slashAnimationSpawnPoint.position,
                Quaternion.identity);
            _slashAnimation.transform.parent = this.transform.parent;
            _animator.SetTrigger("Attack");
            StartCoroutine(AttackRoutine());
        }
    }

    private void StartAttacking()
    {
        _attackButtonPressed = true;            

    }
    
    private void StopAttacking()
    {
        _attackButtonPressed = false;
    }

    public void DoneAttackAnimationEvent()
    {
        _weaponCollider.gameObject.SetActive(false);
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(_playerController.transform.position);

        float angle = (float)(Math.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg);
        if (mousePosition.x < playerPosition.x)
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, 0, angle);

        }
    }

    public void SwingUpFlipAnimationEvent()
    {
        _slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        _slashAnimation.GetComponent<SpriteRenderer>().flipX = _playerController.facingLeft;
    }
    
    public void SwingDownFlipAnimationEvent()
    {
        _slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        _slashAnimation.GetComponent<SpriteRenderer>().flipX = _playerController.facingLeft;
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
