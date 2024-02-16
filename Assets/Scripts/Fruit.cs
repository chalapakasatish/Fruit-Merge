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
        if(collision.collider.TryGetComponent(out Fruit otherFruit))
        {
            if(otherFruit.GetFruitType() != fruitType)
            {
                return;
            }
            onCollisionWithFruit?.Invoke(this,otherFruit);
        }
    }
}
