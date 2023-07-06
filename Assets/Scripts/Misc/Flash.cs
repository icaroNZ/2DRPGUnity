using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private Material whiteFlashMaterial;
    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;
    
    public float getRestoreFlashDuration
    {
        get { return flashDuration; }
    }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

    public void FlashSprite()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = whiteFlashMaterial;
        yield return new WaitForSeconds(flashDuration);
        _spriteRenderer.material = _defaultMaterial;
    }
}
