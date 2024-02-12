using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(FruitManager))]
public class FruiManagerUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI nextFruitText;
    private FruitManager fruitManager;
    // Start is called before the first frame update
    void Start()
    {
        fruitManager = GetComponent<FruitManager>();
    }

    // Update is called once per frame
    void Update()
    {
        nextFruitText.text = fruitManager.GetNextFruitName();
    }
}
