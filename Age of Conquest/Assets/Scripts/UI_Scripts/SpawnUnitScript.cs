using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnitScript : MonoBehaviour
{
    public void SpawnUnit(GameObject unitObject, Vector3 spawnPosition, TeamEnum teamEnum = TeamEnum.AI)
    {
        if (unitObject != null && spawnPosition != null)
        {
            GameObject newUnitObj = Instantiate(unitObject, spawnPosition, Quaternion.identity);
            if (newUnitObj != null)
            {
                newUnitObj.GetComponent<Unit_Properties>().teamEnum = teamEnum;
            }
                    
        }
    }
}
