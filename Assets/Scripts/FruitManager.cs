using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private LineRenderer fruitSpawnLine;
    private GameObject currentFruit;

    [Header("Settings")]
    [SerializeField] private float fruitYSpawnPos;
    public bool enableGizmos;

    private void Start()
    {
        HideLine();
    }
    private void Update()
    {
        ManagePlayerInput();
    }
    private void ManagePlayerInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MouseDownCallback();
        }
        else if(Input.GetMouseButton(0))
        {
            MouseDragCallback();
        }
        else if(Input.GetMouseButtonUp(0)) 
        {
            MouseupCallback();
        }

        
    }

    private void MouseupCallback()
    {
        HideLine();
        currentFruit.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void MouseDragCallback()
    {
        PlaceLineAtClickedPosition();
        currentFruit.transform.position = GetSpawnPosition();
    }
    private Vector2 GetSpawnPosition()
    {
        Vector2 worldClickedPosition = GetClickedWorldPosition();
        worldClickedPosition.y = fruitYSpawnPos;
        return worldClickedPosition;
    }
    private void MouseDownCallback()
    {
        DisplayLine();
        PlaceLineAtClickedPosition();
        SpawnFruit();
    }
    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();
        currentFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void HideLine()
    {
        fruitSpawnLine.enabled = false;
    }
    private void DisplayLine()
    {
        fruitSpawnLine.enabled = true;
    }
    private void PlaceLineAtClickedPosition()
    {
        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizmos)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(-5, fruitYSpawnPos, 0), new Vector3(5, fruitYSpawnPos, 0));
    }
#endif
}
