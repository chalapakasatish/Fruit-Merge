using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Fruit : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Header("Data")]
    [SerializeField] private FruitType fruitType;
    [Header("Actions")]
    public static Action<Fruit,Fruit> onCollisionWithFruit;
    private bool hasCollided;
    private bool canBeMerged;
    [Header("Effects")]
    [SerializeField]private ParticleSystem mergeParticles;
    private void Start()
    {
        Invoke("AllowMerge", 0.25f);
    }
    void AllowMerge()
    {
        canBeMerged = true;
    }
    public void MoveTo(Vector2 targetPosition)
    {
        transform.position = targetPosition;
    }
    public void EnablePhysics()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<CircleCollider2D>().enabled = true;
    }
    public FruitType GetFruitType()
    {
        return fruitType;
    }
    public Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        ManageCollision(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        ManageCollision(collision);
    }
    private void ManageCollision(Collision2D collision)
    {

        hasCollided = true;
        if (!canBeMerged)
        {
            return;
        }
        if (collision.collider.TryGetComponent(out Fruit otherFruit))
        {
            if (otherFruit.GetFruitType() != fruitType)
            {
                return;
            }
            if (!otherFruit.CanBeMerged())
                return;
            onCollisionWithFruit?.Invoke(this, otherFruit);
        }
    }
    public void Merge()
    {
        if (mergeParticles != null)
        {
            mergeParticles.transform.SetParent(null);
            mergeParticles.Play();
        }
        Destroy(gameObject);
    }
    public bool HasCollided()
    {
        return hasCollided;
    }
    public bool CanBeMerged()
    {
        return canBeMerged;
    }
}
