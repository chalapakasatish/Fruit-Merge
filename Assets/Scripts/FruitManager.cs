using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject fruitPrefab;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ManagePlayerInput();
        }
    }
    private void ManagePlayerInput()
    {
        Instantiate(fruitPrefab,GetClickedWorldPosition(),Quaternion.identity);
    }
    private Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
