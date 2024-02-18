using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    [Header("Actions")]
    public static Action<FruitType, Vector2> onMergeProcessed;
    [Header("Settings")]
    public Fruit lastSender;
    // Start is called before the first frame update
    void Start()
    {
        Fruit.onCollisionWithFruit += CollosionBetweenFruitsCallback;
    }
    private void OnDestroy()
    {
        Fruit.onCollisionWithFruit -= CollosionBetweenFruitsCallback;
    }
    private void CollosionBetweenFruitsCallback(Fruit sender,Fruit otherFruit)
    {
        if(lastSender != null)
        {
            return;
        }
        lastSender = sender;
        ProcessMerge(sender,otherFruit);
        //Debug.Log("Collosion" + sender.name);
    }

    private void ProcessMerge(Fruit sender, Fruit otherFruit)
    {
        FruitType mergeFruitType = sender.GetFruitType();
        mergeFruitType += 1;
        Vector2 fruitSpawnPos = (sender.transform.position + otherFruit.transform.position)/2;

        Destroy(sender.gameObject);
        Destroy(otherFruit.gameObject);
        StartCoroutine(ResetLastSenderCoroutine());
        onMergeProcessed?.Invoke(mergeFruitType,fruitSpawnPos);
    }
    IEnumerator ResetLastSenderCoroutine()
    {
        yield return new WaitForEndOfFrame();
        lastSender = null;
    }
}
