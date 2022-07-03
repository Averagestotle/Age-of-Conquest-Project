using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnitScript : MonoBehaviour
{
    public void SpawnUnit(GameObject gameObject, Vector3 spawnPosition)
    {
        if (gameObject != null && spawnPosition != null)
        {
            Instantiate(gameObject, spawnPosition, Quaternion.identity);
        }
    }
}
