using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentifyUnitToSpawn : MonoBehaviour
{
    GameObject unitPrefab;
    SpawnUnitScript spawnUnitScript;

    public void FindUnitToInstantiate(EraEnums eraEnum, GameObject unitObject, Vector3 spawnPosition)
    {
        spawnUnitScript = new SpawnUnitScript();
        


        switch(eraEnum)
        {
            case EraEnums.Era01:
                if (unitObject != null)
                {
                    spawnUnitScript.SpawnUnit((GameObject)unitObject, spawnPosition);
                }

                break;
            default:

                break;
        }        
    }
}
